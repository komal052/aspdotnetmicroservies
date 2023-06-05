using AspnetRunBasics.Models;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace AspnetRunBasics.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseModel>> GetOrderByUserName(string userName);
    }
}
