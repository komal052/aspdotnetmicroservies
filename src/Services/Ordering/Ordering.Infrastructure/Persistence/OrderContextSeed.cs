using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public  class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext context, ILogger<OrderContextSeed> logger)
        {
            if(!context.Order.Any())
            {
                context.Order.AddRange(GetPreConfiguredOrders());
                await context.SaveChangesAsync();
                logger.LogInformation("seed database asscoiated with context", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreConfiguredOrders()
        {
            return new List<Order>()
            {
                new Order() { UserName="komal12", FirstName="komal", LastName="Patel", EmailAddress="komal@gmail.com", State="gujarat",ZipCode="395007",
                    CreatedBy="null",CreatedDate=DateTime.UtcNow
                ,Country="india",TotalPrice=500, CVV="null", CardName="null", PaymentMethod="1", CardNumber="null",Expiration="null",LastModifiedBy="null"
                }
            };
        }

    }
}
