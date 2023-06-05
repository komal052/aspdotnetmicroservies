using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using Catalog.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics.Pages
{
    public class CartModel : PageModel
    {
        private readonly IBasketService _basketService;

        public CartModel(IBasketService basketService)
        {
            _basketService = basketService;
        }
        public BasketModel Cart { get; set; }=new BasketModel();

        public async Task<IActionResult> OnGetAsync(string productId)
        {
            var userName = "niti";
            Cart = await _basketService.GetBasket(userName);
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartasync(string ProductId)
        {
            var userName = "niti";
            var basket = await _basketService.GetBasket(userName); 
            var item =basket.Items.Single(x=>x.ProductId== ProductId);
            basket.Items.Remove(item);
            var basketupdate = await _basketService.UpdateBasket(basket);
            return RedirectToPage();    
        }
    }
}
