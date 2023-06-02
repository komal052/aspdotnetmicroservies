﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;
using System.Net;

namespace Shopping.Aggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public ShoppingController(ICatalogService catalogService, IBasketService basketService, IOrderService orderService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
            _orderService = orderService;
        }
        [HttpGet("{userName}", Name ="GetShopping")]
        [ProducesResponseType(typeof(ShoppingModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingModel>> GetShopping(string userName)
        {
            var basket= await _basketService.GetBasket(userName);

            foreach(var item in basket.Items) {

                    var product = await _catalogService.GetCatalog(item.ProductId) ;
                item.ProductName=product.Name;
                item.Category = product.Category;
                item.Summary=product.Summary;
                item.Description = product.Description;
                item.ImageFile = product.ImageFile; 

            } 
             var order=await _orderService.GetOrderByUserName(userName);
            var ShoppingModel = new ShoppingModel
            {
                UserName = userName,
                BasketWithProduct = basket,
                Orders = order
               
            };
            return Ok(ShoppingModel);
        }
    }
}
