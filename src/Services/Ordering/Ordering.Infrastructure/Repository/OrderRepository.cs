using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Order>> GetOrderByUsername(string username)
        {
            var ordrList = await _context.Order.Where(x => x.UserName == username).ToListAsync();
            return ordrList;

        }
    }
}
