using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics.Pages
{
    public class ProductModel : PageModel
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public ProductModel(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        public IEnumerable<string> CategoryList { get; set; } =new List<string>();
        public IEnumerable<CatalogModel> ProductList { get; set; }=new List<CatalogModel>();

        [BindProperty(SupportsGet =true)]
        public string SelectedCategory { get; set; }


        public async Task<IActionResult> OnGetAsync(string categoryName)
        {
            var ProductList = await _catalogService.GetCatalog();
            CategoryList = ProductList.Select(x => x.Category).Distinct();

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                ProductList = ProductList.Where(x => x.Category == categoryName);
                SelectedCategory = categoryName;
            }
            else
            {
                ProductList = ProductList;
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
