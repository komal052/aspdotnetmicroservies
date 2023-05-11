using Catalog.API.Data;
using Catalog.API.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Xml.Linq;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
         
        public   async Task CreateProduct(Product product)
        {
            await  _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string Id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x=>x.Id,Id);
            DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount> 0;

        }

        public async Task<Product> GetProduct(string Id)
        {
            return _context.Products.Find(x => x.Id == Id).FirstOrDefault();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string category)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Category, category);
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string Name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Name, Name);
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return _context.Products.Find(x=>true).ToList();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
          var result= _context.Products.ReplaceOne(filter:g=>g.Id==product.Id,replacement:product);

            return result.IsAcknowledged && result.ModifiedCount>0;
        }
    }
}
