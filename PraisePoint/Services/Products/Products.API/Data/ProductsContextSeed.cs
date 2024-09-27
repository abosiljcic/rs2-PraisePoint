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
                    ImageUrl = "https://media.istockphoto.com/id/483960103/photo/blank-black-t-shirt-front-with-clipping-path.jpg?s=612x612&w=0&k=20&c=d8qlXILMYhugXGw6zX7Jer2SLPrLPORfsDsfRDWc-50=",
                    Price = 40,
                },
                new Product()
                {
                    Id = Guid.Parse("c0289dc4-323f-4dea-b056-c0fd54e8fbf7"),
                    Name = "Backpack",
                    ImageUrl = "https://images.unsplash.com/photo-1528384483229-b4a97480dbea?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8OTB8fGJhY2twYWNrfGVufDB8fDB8fHww",
                    Price = 70,
                },
                new Product()
                {
                    Id = Guid.Parse("a6c547ef-dca9-4803-ba62-0690d34eba66"),
                    Name = "Umbrella",
                    ImageUrl = "https://www.careofcarl.com/bilder/artiklar/zoom/13074910_1.jpg?m=1683116487",
                    Price = 15,
                },
                new Product()
                {
                    Id = Guid.Parse("2694568d-3d25-4d5d-bc03-38ed52aef86b"),
                    Name = "Water bottle",
                    ImageUrl = "https://www.camelbak.com/dw/image/v2/BDBJ_PRD/on/demandware.static/-/Sites-camelbak-master-catalog/default/dwa4aa83af/images/large/1650001001.jpg?sw=650&sh=650&sm=fit",
                    Price = 20,
                },
            };
        }
    }
}
