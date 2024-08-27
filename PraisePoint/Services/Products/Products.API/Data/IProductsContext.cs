using MongoDB.Driver;
using Products.API.Entities;

namespace Products.API.Data
{
    public interface IProductsContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
