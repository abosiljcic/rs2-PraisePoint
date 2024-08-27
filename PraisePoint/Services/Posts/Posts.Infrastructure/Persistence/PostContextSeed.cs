using Microsoft.Extensions.Logging;
using Posts.Domain.Aggregates;

namespace Posts.Infrastructure.Persistence
{
    public class PostContextSeed
    {
        public static async Task SeedAsync(PostContext postContext, ILogger<PostContextSeed> logger)
        {
            if (!postContext.Posts.Any())
            {
                postContext.Posts.AddRange(GetPreconfiguredPosts());
                await postContext.SaveChangesAsync();
                logger.LogInformation("Seeding database associated with context {DbContextName}", nameof(PostContext));
            }
        }

        private static IEnumerable<Post> GetPreconfiguredPosts()
        {
            var post1 = new Post("nikolina", "kaca", Guid.Parse("18809c4c-f5d3-421a-9a4e-0ac08b247352"), 20, "Resila tezak bag");
            post1.ToggleLiked("ilija");
            post1.ToggleLiked("andrijana");
            post1.ToggleLiked("andjela");


            post1.AddComment("ilija", "supeer bravo");

            var post2 = new Post("ilija", "andrijana", Guid.Parse("18809c4c-f5d3-421a-9a4e-0ac08b247352"), 15, "Skuva mi uvek ujutru kafu");
            post2.ToggleLiked("nikolina");
            post2.ToggleLiked("andrijana");
            post2.ToggleLiked("andjela");


            post2.AddComment("kaca", "lepoo");
            post2.AddComment("nikolina", "dobar kolega");

            var post3 = new Post("kaca", "maja", Guid.Parse("18809c4c-f5d3-421a-9a4e-0ac08b247352"), 25, "Brzo je zavrsila task koji mi je bio potreban da radim dalje");
            post3.ToggleLiked("andrijana");
            post3.ToggleLiked("maja");


            post3.AddComment("ilija", "wow");
            post3.AddComment("nikolina", "i meni je znacilo");

            return new List<Post> { post1, post2, post3 };
        }
    }
}
