using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Movies.Client.HttpHandlers;
using WebApp.Client.ApiServices;
using WebApp.Client.ApiServices.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDiscountApiService, DiscountApiService>();

// http operations
// 1 create an HttpClient used for accessing the Movies.API
builder.Services.AddTransient<AuthenticationDelegatingHandler>();
builder.Services.AddHttpClient("DiscountAPIClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:5010/"); // API GATEWAY URL
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
}).AddHttpMessageHandler<AuthenticationDelegatingHandler>();
// 2 create an HttpClient used for accessing the IDP
builder.Services.AddHttpClient("IDPClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:5005/"); //Identity URL
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
    {
        options.Authority = "https://localhost:5005";

        options.ClientId = "Webapp_mvc_client";
        options.ClientSecret = "secret";
        options.ResponseType = "code id_token";

        //options.Scope.Add("openid");
        //options.Scope.Add("profile");
        options.Scope.Add("address");
        options.Scope.Add("email");
        options.Scope.Add("roles");

        options.ClaimActions.DeleteClaim("sid");
        options.ClaimActions.DeleteClaim("idp");
        options.ClaimActions.DeleteClaim("s_hash");
        options.ClaimActions.DeleteClaim("auth_time");
        options.ClaimActions.MapUniqueJsonKey("role", "role");

        options.Scope.Add("movieAPI");

        options.SaveTokens = true;
        options.GetClaimsFromUserInfoEndpoint = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = JwtClaimTypes.GivenName,
            RoleClaimType = JwtClaimTypes.Role
        };
    });
builder.Services.AddSingleton(new ClientCredentialsTokenRequest
{
    Address = "https://localhost:5005/connect/token",
    ClientId = "discountClient",
    ClientSecret = "toiyeuem_secret",
    Scope = "discountAPI"
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
