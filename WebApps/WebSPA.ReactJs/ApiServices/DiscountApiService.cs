using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using WebSPA.ReactJs.ApiServices.Interface;
using WebSPA.ReactJs.Interface;
using WebSPA.ReactJs.Models;

namespace WebSPA.ReactJs.ApiServices
{
    public class DiscountApiService : IDiscountApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthApiService _authApiService;
        public DiscountApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IAuthApiService authApiService)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _authApiService = authApiService;
        }

        public Task<bool> CreateDiscount(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDiscount(string productName)
        {
            throw new NotImplementedException();
        }
        public async Task<Coupon> GetDiscount(string productName)
        {
            var httpClient = _httpClientFactory.CreateClient("DiscountAPIClient");
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "/api/Discount/IPhone X");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authApiService.GetToken());
            var response = await httpClient.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var coupons = JsonConvert.DeserializeObject<Coupon>(content);
                return coupons!;
            }
            return new Coupon {};
        }

        public async Task<UserInfoViewModel> GetUserInfo()
        {
            var idpClient = _httpClientFactory.CreateClient("IDPClient");

            var metaDataResponse = await idpClient.GetDiscoveryDocumentAsync();

            if (metaDataResponse.IsError)
            {
                throw new HttpRequestException("Something went wrong while requesting the access token");
            }

            var accessToken = await _httpContextAccessor
                .HttpContext!.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            
            var userInfoResponse = await idpClient.GetUserInfoAsync(
               new UserInfoRequest
               {
                   Address = metaDataResponse.UserInfoEndpoint,
                   Token = accessToken
               });

            if (userInfoResponse.IsError)
            {
                throw new HttpRequestException("Something went wrong while getting user info");
            }

            var userInfoDictionary = new Dictionary<string, string>();

            foreach (var claim in userInfoResponse.Claims)
            {
                userInfoDictionary.Add(claim.Type, claim.Value);
            }

            return new UserInfoViewModel(userInfoDictionary);
        }
        public Task<bool> UpdateDiscount(Coupon coupon)
        {
            throw new NotImplementedException();
        }
    }
}
