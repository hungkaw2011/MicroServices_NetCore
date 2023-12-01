using WebApp.Client.Models;

namespace WebApp.Client.ApiServices.Interface
{
    public interface IDiscountApiService
    {
        Task<Coupon> GetDiscount(string productName);
        Task<bool> CreateDiscount(Coupon coupon);
        Task<bool> UpdateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string productName);
        Task<UserInfoViewModel> GetUserInfo();
    }
}
