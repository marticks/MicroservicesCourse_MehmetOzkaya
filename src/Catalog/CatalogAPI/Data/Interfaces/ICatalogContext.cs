using CatalogAPI.Entities;
using MongoDB.Driver;

namespace CatalogAPI.Data.Interfaces
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products {get;}
    }
}