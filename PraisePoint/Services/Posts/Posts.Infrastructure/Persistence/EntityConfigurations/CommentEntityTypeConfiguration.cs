using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Posts.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Posts.Infrastructure.Persistence.EntityConfigurations
{
    public class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comment");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).UseHiLo("commentseq");

            builder.Property<string>("Username")
                .HasColumnType("VARCHAR(24)")
                .HasColumnName("Username")
                .IsRequired();

            builder.Property<string>("Text")
                .HasColumnType("VARCHAR(512)")
                .HasColumnName("Username")
                .IsRequired();
        }
    }
}
