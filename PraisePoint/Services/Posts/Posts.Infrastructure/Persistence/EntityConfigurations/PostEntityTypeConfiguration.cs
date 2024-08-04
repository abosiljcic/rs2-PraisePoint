using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Posts.Domain.Aggregates;
using Posts.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Posts.Infrastructure.Persistence.EntityConfigurations
{
    public class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
    {

        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).UseHiLo("postseq");

            builder.OwnsMany(o => o.PostLikes, a =>
            {
                a.Property<int>("PostId").UseHiLo("postseq"); 
                a.WithOwner().HasForeignKey("PostId");
            });

            builder.OwnsMany(o => o.PostComments, a =>
            {
                a.Property<int>("PostId").UseHiLo("postseq"); 
                a.WithOwner().HasForeignKey("PostId");
            });

            var navigation1 = builder.Metadata.FindNavigation(nameof(Post.PostLikes))
                             ?? throw new PostDomainException($"No navigation property found on {nameof(Post.PostLikes)}");
            navigation1.SetPropertyAccessMode(PropertyAccessMode.Field);

            var navigation2 = builder.Metadata.FindNavigation(nameof(Post.PostComments))
                             ?? throw new PostDomainException($"No navigation property found on {nameof(Post.PostComments)}");
            navigation2.SetPropertyAccessMode(PropertyAccessMode.Field);
        }

    }
}
