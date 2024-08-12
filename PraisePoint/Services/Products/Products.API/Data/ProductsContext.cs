using MongoDB.Driver;
using Products.API.Entities;

namespace Products.API.Data
{
    public class ProductsContext : IProductsContext
    {
        public ProductsContext() 
        { 
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("ProductsDB");

            Products = database.GetCollection<Product>("Products");
            ProductsContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
