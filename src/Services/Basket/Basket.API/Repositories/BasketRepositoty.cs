using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _rediscache;

        public BasketRepository(IDistributedCache rediscache)
        {
            _rediscache = rediscache ?? throw new ArgumentNullException(nameof(rediscache));
        }

        public async Task deleteBasket(string name)
        {
           await _rediscache.RemoveAsync(name);
        }

        public async Task<ShoppingCart> getBasket(string name)
        {
            var basket= await _rediscache.GetStringAsync(name);
            if (string.IsNullOrEmpty(basket))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
           await _rediscache.SetStringAsync(basket.UserName,JsonConvert.SerializeObject(basket));
            return await getBasket(basket.UserName);
        }
    }
}
