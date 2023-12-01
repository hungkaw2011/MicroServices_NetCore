using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Data;
using System.Diagnostics;
using WebApp.Client.ApiServices;
using WebApp.Client.ApiServices.Interface;

namespace WebApp.Client.Controllers
{
    [Authorize]
    public class DiscountController : Controller
    {
        private readonly IDiscountApiService _discountService;

        public DiscountController(IDiscountApiService discountService)
        {
            _discountService = discountService;
        }
        public async Task<IActionResult> Index(string productName)
        {
            await LogTokenAndClaims();
            var userInfo = await _discountService.GetUserInfo();
            foreach (var item in userInfo.UserInfoDictionary)
            {
                if (item.Key== "name")
                {
                    ViewBag.UserName = item.Value;
                    break;
                }
            }
            return View(await _discountService.GetDiscount(productName));
        }
        public async Task LogTokenAndClaims()
        {
            var identityToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);

            Debug.WriteLine($"Identity token: {identityToken}");

            foreach (var claim in User.Claims)
            {
                Debug.WriteLine($"Claim type: {claim.Type} - Claim value: {claim.Value}");
            }
        }
        public async Task<IActionResult> OnlyAdmin()
        {
            var userInfo = await _discountService.GetUserInfo();
            return View(userInfo);
        }
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}
