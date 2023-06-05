using Amazon.SecurityToken.Model.Internal.MarshallTransformations;
using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics.Pages
{
    public class CheckOutModel : PageModel
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public CheckOutModel(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }

        [BindProperty]
        public BasketCheckoutModel Order { get; set; }

        public BasketModel Cart { get; set; } = new BasketModel();

        public async Task<IActionResult> OnGetAsync()
        {
            var userName = "niti";
            Cart=await _basketService.GetBasket(userName);
            return Page();
        }
        public async Task<IActionResult> OnPostAddToCartAstnc(string productId)
        {
            var userName = "niti";
            Cart = await _basketService.GetBasket(userName);

            if(!ModelState.IsValid)
            {
                return Page();
            }
            Order.UserName= userName;
            Order.TotalPrice = Cart.TotalPrice;

            await _basketService.CheckoutBasket(Order);
            return RedirectToPage("Confirmation", "OrderSubmitted");
        }
    }
}
