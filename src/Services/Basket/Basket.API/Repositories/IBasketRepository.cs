using Basket.API.Entities;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> getBasket(string name);
        Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
        Task deleteBasket(string name);
    }
}
