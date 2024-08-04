using Microsoft.Extensions.Logging;
using Posts.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
            var post1 = new Post("nikolina", "kaca", Guid.NewGuid(), 20, "Resila tezak bag");
            post1.AddLike("ilija");
            post1.AddLike("andrijana");
            post1.AddLike("andjela");


            post1.AddComment("ilija", "supeer bravo");

            return new List<Post> { post1 };
        }
    }
}
