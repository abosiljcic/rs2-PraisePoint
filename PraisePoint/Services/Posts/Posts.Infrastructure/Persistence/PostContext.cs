using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Posts.Domain.Aggregates;
using Posts.Domain.Common;
using Posts.Domain.Entities;
using Posts.Infrastructure.Persistence.EntityConfigurations;

namespace Posts.Infrastructure.Persistence
{
    public class PostContext : DbContext
    {
        public DbSet<Post> Posts { get; set; } = null!;
        // public DbSet<Like> Likes { get; set; } = null;
        // public DbSet<Comment> Comments { get; set; } = null;

        public PostContext(DbContextOptions options) : base(options)
        {
        }

        //ako imamo poseban kod koji zelimo da se izvrsi svaki put kada se nasi entiteti cuvaju(imaju i druge add update....)
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        //entry.Entity.CreatedBy = "rs2";   ovo kad se ubace korisnici (da li treba da dodamo to polje?)
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        //entry.Entity.LastModifiedBy = "rs2";
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PostEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LikeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CommentEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
