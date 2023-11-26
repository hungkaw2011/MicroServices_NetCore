using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Diagnostics;
using WebApp.Client.ApiServices;
using WebApp.Client.ApiServices.Interface;

namespace WebApp.Client.Controllers
{
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
    }
}
