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
    public class LikeEntityTypeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.ToTable("Like");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).UseHiLo("likeseq");

            builder.Property<string>("Username")
                .HasColumnType("VARCHAR(24)")
                .HasColumnName("Username")
                .IsRequired();
        }
    }
}
