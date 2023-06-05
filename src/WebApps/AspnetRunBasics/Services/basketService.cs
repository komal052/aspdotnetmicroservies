using AspnetRunBasics.Extension;
using AspnetRunBasics.Models;

namespace AspnetRunBasics.Services
{
    public class basketService : IBasketService
    {
        private readonly HttpClient _httpClient;

        public basketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CheckoutBasket(BasketCheckoutModel model)
        {
            var response = await _httpClient.PostAsJson($"/Basket/Checkout", model);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Something went wrong on api Calling");
            }
        }

        public async Task<BasketModel> GetBasket(string userName)
        {
            var response = await _httpClient.GetAsync($"/Basket/{userName}");
            return await response.ReadContentAs<BasketModel>();
        }

        public async Task<BasketModel> UpdateBasket(BasketModel model)
        {
            var response = await _httpClient.PostAsJson("/Basket", model);
            if(response.IsSuccessStatusCode) {
                return await response.ReadContentAs<BasketModel>();
            }
            else
            {
                throw new Exception("Something went wrong on api Calling");
            }
        }
    }
}
