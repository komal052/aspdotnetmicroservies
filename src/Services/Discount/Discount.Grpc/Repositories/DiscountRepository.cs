using Dapper;
using Discount.Grpc;
using Discount.Grpc.Entities;
using Npgsql;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {

        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration) { 
            _configuration = configuration;
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affected = await connection.ExecuteAsync("insert into coupon (ProductName, description ,amount) values (@ProductName, @Description, @Amount)", new { ProductName=coupon.ProductName, description = coupon.Description, amount = coupon.Amount});
            if(affected==0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affected = await connection.ExecuteAsync("Delete from coupon where ProductName=@ProductName ", new { ProdcutName = productName});
            if (affected == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<Coupon> GetDiscount(string productname)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("select * from Coupon where ProductName=@ProductName", new { ProductName=productname});
            if(coupon == null) 
            { 
                return new Coupon { ProductName="no discount", Description="No discount disc",Amount= 0};
            }
            return coupon;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affected = await connection.ExecuteAsync("update Coupon set ProductName=@ProductName, Description=@Description,Amount=@Amount where Id=@Id ", new { ProdcutName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });
            if (affected == 0)
            {
                return false;
            }
            return true;
        }
    }
}
