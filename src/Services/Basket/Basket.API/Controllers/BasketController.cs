using Basket.API.Entities;
using Basket.API.GrpcService;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Xml.Linq;

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
         private readonly DiscountGrpcService _discountGrpcService;

        public BasketController(IBasketRepository basketRepository,
            DiscountGrpcService discountGrpcService)
        {
            _basketRepository = basketRepository?? throw new ArgumentNullException(nameof(basketRepository));
            _discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)] 
        public async Task<ActionResult<ShoppingCart>> Get(string name)
        {
            var basket = await _basketRepository.getBasket(name); 
            return Ok(basket ?? new ShoppingCart(name));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            //communicate with discount.grpc

            foreach (var item in basket.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }

            return Ok(await _basketRepository.UpdateBasket(basket));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> DeleteBasket(string name)
        {
           await _basketRepository.deleteBasket(name);
            return Ok();
        }
    }
}
