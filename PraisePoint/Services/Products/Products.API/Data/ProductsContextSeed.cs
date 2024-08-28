using MongoDB.Driver;
using Products.API.Entities;

namespace Products.API.Data
{
    public class ProductsContextSeed
    {
        public static void SeedData(IMongoCollection<Product> products)
        {
            var existProducts = products.Find(p => true).Any();
            if (!existProducts)
            {
                products.InsertManyAsync((GetConfiguredProducts()));
            }
        }

        private static IEnumerable<Product> GetConfiguredProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = Guid.Parse("f930e082-a324-4823-bfb3-f1c192ef30b3"),
                    Name = "T-shirt",
                    ImageUrl = "product-1.png",
                    Price = 40,
                },
                new Product()
                {
                    Id = Guid.Parse("c0289dc4-323f-4dea-b056-c0fd54e8fbf7"),
                    Name = "Backpack",
                    ImageUrl = "product-2.png",
                    Price = 70,
                },
                new Product()
                {
                    Id = Guid.Parse("a6c547ef-dca9-4803-ba62-0690d34eba66"),
                    Name = "Umbrella",
                    ImageUrl = "product-3.png",
                    Price = 15,
                },
                new Product()
                {
                    Id = Guid.Parse("2694568d-3d25-4d5d-bc03-38ed52aef86b"),
                    Name = "Water bottle",
                    ImageUrl = "product-4.png",
                    Price = 20,
                },
            };
        }
    }
}
