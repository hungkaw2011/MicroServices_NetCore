using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using WebSPA.ReactJs.ApiServices;
using WebSPA.ReactJs.ApiServices.Interface;
using WebSPA.ReactJs.HttpHandlers;
using WebSPA.ReactJs.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDiscountApiService, DiscountApiService>();
builder.Services.AddScoped<IAuthApiService, AuthApiService>();
builder.Services.AddTransient<AuthenticationDelegatingHandler>();

// http operations
// 1 create an HttpClient used for accessing the Discount.API
builder.Services.AddHttpClient("DiscountAPIClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:5010/"); // API GATEWAY URL
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
}).AddHttpMessageHandler<AuthenticationDelegatingHandler>();
// 2 create an HttpClient used for accessing the IDP
builder.Services.AddHttpClient("IDPClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:5005/"); //Identity Provider URL
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton(new ClientCredentialsTokenRequest
{
    Address = "https://localhost:5005/connect/token",
    ClientId = "discountClient",
    ClientSecret = "toiyeuem_secret",
    Scope = "discountAPI"
});
#region AddAuthenticationService
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
    {
        options.Authority = "https://localhost:5005";

        options.ClientId = "webapp_mvc_client";
        options.ClientSecret = "toiyeuem_secret";
        options.ResponseType = "code id_token";
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("address");
        options.Scope.Add("email");
        options.Scope.Add("roles");

        options.ClaimActions.DeleteClaim("sid");
        options.ClaimActions.DeleteClaim("idp");
        options.ClaimActions.DeleteClaim("s_hash");
        options.ClaimActions.DeleteClaim("auth_time");
        options.ClaimActions.MapUniqueJsonKey("role", "role");
        options.Scope.Add("discountAPI");

        options.SaveTokens = true;
        options.GetClaimsFromUserInfoEndpoint = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = JwtClaimTypes.GivenName,
            RoleClaimType = JwtClaimTypes.Role
        };
    });
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
