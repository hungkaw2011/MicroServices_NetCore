using Duende.IdentityServer;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Services;
using IdentityModel;
using IdentityServer.Models;
using IdentityServerAspNetIdentity.Data;
using IdentityServerHost;
using IdentityServerHost.Pages.Admin.ApiScopes;
using IdentityServerHost.Pages.Admin.Clients;
using IdentityServerHost.Pages.Admin.IdentityScopes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Serilog;
using System.Data;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace IdentityServer
{
    public static class HostingExetensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            builder.Services
                .AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                    options.EmitStaticAudienceClaim = true;
                })
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddAspNetIdentity<ApplicationUser>();
            builder.Services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    // register your IdentityServer with Google at https://console.developers.google.com
                    // enable the Google+ API
                    // set the redirect URI to https://localhost:5005/signin-google // IdentityServer Url
                    options.ClientId = "273665303295-lgg3b2rvddtto09uv3j5qfide3ciqubs.apps.googleusercontent.com";
                    options.ClientSecret = "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";
                    options.Events = new OAuthEvents
                    {
                        OnCreatingTicket = async context =>
                        {
                            var roles = new List<string> { "User" };
                            var email = context.Identity!.FindFirst(ClaimTypes.Email)?.Value;
                            var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
                            var user = await userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
                            if (user == null)
                            {
                                user = new ApplicationUser { UserName = email, Email = email };
                                await userManager.CreateAsync(user);
                            }
                            await userManager.AddToRolesAsync(user, roles);
                            //var claims = new List<Claim>
                            //{
                            //    new Claim(ClaimTypes.NameIdentifier, user.Id),
                            //    new Claim(ClaimTypes.Name, user.UserName!),
                            //    new Claim(ClaimTypes.Email, user.Email!)
                            //};
                            //claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
                            //context.Identity.AddClaims(claims);
                        }
                    };
                })
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = "653362827008223";
                    facebookOptions.AppSecret = "4b455a48ceda1f7528a055f29bc267d5";
                })
                .AddOAuth("GitHub", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = "0a2879dd671db0773cc7";
                    options.ClientSecret = "270a38c5cbdeaa4743ac74c81403e5bbbf3776c3";
                    options.CallbackPath = new PathString("/github-oauth");
                    options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
                    options.TokenEndpoint = "https://github.com/login/oauth/access_token";
                    options.UserInformationEndpoint = "https://api.github.com/user";
                    options.SaveTokens = true;
                    options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                    options.ClaimActions.MapJsonKey(ClaimTypes.Name, "login");
                    options.ClaimActions.MapJsonKey("urn:github:login", "login");
                    options.ClaimActions.MapJsonKey("urn:github:id", "id");
                    options.ClaimActions.MapJsonKey("urn:github:avatar", "avatar_url");
                    options.ClaimActions.MapJsonKey("urn:github:url", "html_url");
                    options.Events = new OAuthEvents
                    {
                        OnRemoteFailure = context =>
                        {
                            context.Response.Redirect("/");
                            context.HandleResponse();
                            return Task.CompletedTask;
                        },
                        OnCreatingTicket = async context =>
                        {
                            var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
                            var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                            response.EnsureSuccessStatusCode();
                            var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                            context.RunClaimActions(json.RootElement);

                            var roles = new List<string> { "User" };
                            var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
                            var user = await userManager.FindByLoginAsync("Github", context.Principal!.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                            if (user != null)
                            {
                                if (!await userManager.IsInRoleAsync(user, "User"))
                                {
                                    await userManager.AddToRolesAsync(user, roles);
                                }
                            }
                        }
                    };
                }); ;
            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.MapRazorPages()
                .RequireAuthorization();

            return app;
        }
    }
}
