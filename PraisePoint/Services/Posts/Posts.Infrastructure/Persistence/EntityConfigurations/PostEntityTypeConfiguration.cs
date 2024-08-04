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
            // Configure the table name
            builder.ToTable("Posts");

            // Configure the primary key
            builder.HasKey(p => p.Id);

            // Configure properties
            builder.Property(p => p.SenderUsername)
                .HasColumnType("VARCHAR(24)")
                .HasColumnName("SenderUsername")
                .IsRequired();

            builder.Property(p => p.ReceiverUsername)
                .HasColumnType("VARCHAR(24)")
                .HasColumnName("ReceiverUsername")
                .IsRequired();

            builder.Property(p => p.CompanyId)
                .HasColumnType("uniqueidentifier")
                .HasColumnName("CompanyId")
                .IsRequired();

            builder.Property(p => p.Points)
                .HasColumnType("int")
                .HasColumnName("Points")
                .IsRequired();

            builder.Property(p => p.Description)
                .HasColumnType("VARCHAR(1000)")
                .HasColumnName("Description")
                .IsRequired();

            // builder.HasMany(p => p.PostLikes)
            //     .WithOne(l => l.Post)
            //     .HasForeignKey(l => l.PostId);
            //
            // builder.HasMany(p => p.PostComments)
            //     .WithOne(c => c.Post)
            //     .HasForeignKey(c => c.PostId);
            var likesNavigation = builder.Metadata.FindNavigation(nameof(Post.PostLikes))
                             ?? throw new PostDomainException($"No navigation property found on {nameof(Post.PostLikes)}");
            likesNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
            
            var commentsNavigation = builder.Metadata.FindNavigation(nameof(Post.PostComments))
                             ?? throw new PostDomainException($"No navigation property found on {nameof(Post.PostComments)}");
            commentsNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }

    }
}
