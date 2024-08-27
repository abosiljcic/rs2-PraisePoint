using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Products.API.Entities;

namespace Products.API.Data
{
    public class ProductsContext : IProductsContext
    {
        public ProductsContext(IConfiguration configuration)
        { 
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionSettings"));
            var database = client.GetDatabase("ProductsDB");

            Products = database.GetCollection<Product>("Products");
            ProductsContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
