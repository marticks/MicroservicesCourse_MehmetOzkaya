using System.Collections.Generic;
using System.Threading.Tasks;
using CatalogAPI.Entities;

namespace CatalogAPI.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(string id);

        Task<IEnumerable<Product>> GetProductByName(string name);

        
        Task<IEnumerable<Product>> GetProductByCategory(string categoryName);

        Task Create(Product product); // podría devolver el recurso generado...

        Task<bool> Update(Product product);

        Task<bool> Delete (string id); // podría recibir el Id...
    }
}