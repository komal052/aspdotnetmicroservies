using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public IndexModel(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }
        public IEnumerable<CatalogModel> ProductList { get; set; }=new List<CatalogModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            ProductList =await _catalogService.GetCatalog();
            return Page();
        }
        public async Task<IActionResult> OnPostAddToCartAstnc(string ProductId)
        {
            var product=await _catalogService.GetCatalog(ProductId);
            var userName = "niti";
            var basket = await _basketService.GetBasket(userName);
            basket.Items.Add(new BasketItemModel
            {
                ProductId = ProductId,
                ProductName = product.Name,
                Price= product.Price,
                Color="black",
                Quantity=1
            }) ;

            var updatebasket = await _basketService.UpdateBasket(basket);
            return RedirectToPage("Cart");
        }
    }
}