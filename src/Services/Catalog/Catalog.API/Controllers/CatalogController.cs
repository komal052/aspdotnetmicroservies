using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogController> _logger;    

        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
        {
            //_productRepository = productRepository;
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> getProducts()
        {
            var product=await _productRepository.GetProducts();
            return Ok(product);
        }
        [HttpGet("{id:length(24)}",Name="Getproduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductById(String Id)
        {
            var product = await _productRepository.GetProduct(Id);
            if(product==null) {
                _logger.LogError($"Product id:{Id}, not found");
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("ProductByCategory")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> getProductByCategory(String category)
        {
            var product = await _productRepository.GetProductByCategory(category);
            return Ok(product);
        }
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> CreateProduct([FromBody] Product product)
        {
            await _productRepository.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct ([FromBody] Product product)
        {
           return Ok( await _productRepository.UpdateProduct(product));
        }

        [HttpDelete("{id:length(24)}", Name ="DeleteProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(string Id)
        {
            return Ok(await _productRepository.DeleteProduct(Id));
        }


    }
}
