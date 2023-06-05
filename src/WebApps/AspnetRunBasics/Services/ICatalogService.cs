using AspnetRunBasics.Models;

namespace AspnetRunBasics.Services
{
    public interface ICatalogService
    {
        Task<CatalogModel> GetCatalog(string id);
        Task<IEnumerable<CatalogModel>> GetCatalog();
        Task<IEnumerable<CatalogModel>> GetCatalogBycategory(string category);
        Task<CatalogModel> CreateCatalog(CatalogModel model);
    }
}
