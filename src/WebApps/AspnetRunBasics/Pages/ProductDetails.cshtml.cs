using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics.Pages
{
    public class ProductDetailsModel : PageModel
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public ProductDetailsModel(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }
        public CatalogModel Product { get; set; }

        [BindProperty]
        public string Color { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        public async Task<IActionResult> OnGetAsync(string productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            Product = await _catalogService.GetCatalog(productId);
            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAstnc(string ProductId)
        {
            var product = await _catalogService.GetCatalog(ProductId);
            var userName = "niti";
            var basket = await _basketService.GetBasket(userName);
            basket.Items.Add(new BasketItemModel
            {
                ProductId = ProductId,
                ProductName = product.Name,
                Price = product.Price,
                Color = "black",
                Quantity = 1
            });

            var updatebasket = await _basketService.UpdateBasket(basket);
            return RedirectToPage("Cart");
        }
    }
}
