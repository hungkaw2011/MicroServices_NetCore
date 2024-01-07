using WebSPA.ReactJs.Models;

namespace WebSPA.ReactJs.Interface
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
