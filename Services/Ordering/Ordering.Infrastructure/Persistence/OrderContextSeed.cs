using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            try
            {
                if (!orderContext.Orders.Any())
                {
                    orderContext.Orders.AddRange(GetPreconfiguredOrders());
                    await orderContext.SaveChangesAsync();
                    logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
                }
            }
            catch (Exception) { }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() {UserName = "Hùng", FirstName = "Nguyễn", LastName = "", EmailAddress = "hungqaz38@gmail.com",
                    AddressLine = "Hà Tĩnh", Country = "VN", TotalPrice = 350,CVV="",CardName="",CardNumber="",
                    Expiration = "",State = "",ZipCode= "",PaymentMethod=0},
                new Order() {UserName = "Hùng", FirstName = "Nguyễn", LastName = "", EmailAddress = "hungqaz38@gmail.com",
                    AddressLine = "Hà Tĩnh", Country = "VN", TotalPrice = 350,CVV="",CardName="",CardNumber="",
                    Expiration = "",State = "",ZipCode= "",PaymentMethod=0}
            };
        }
    }
}
