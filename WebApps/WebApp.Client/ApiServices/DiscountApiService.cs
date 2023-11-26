using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using WebApp.Client.ApiServices.Interface;
using WebApp.Client.Models;

namespace WebApp.Client.ApiServices
{
    public class DiscountApiService : IDiscountApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DiscountApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<bool> CreateDiscount(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDiscount(string productName)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Coupon>> GetDiscount(string productName)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            var httpClient = _httpClientFactory.CreateClient("DiscountAPIClient");
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"api/Discount/{productName}");
            var response = await httpClient.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var coupons = JsonConvert.DeserializeObject<List<Coupon>>(content);
                return coupons!;
            }
            return new List<Coupon> { };

        }

        public Task<bool> UpdateDiscount(Coupon coupon)
        {
            throw new NotImplementedException();
        }
    }
}
