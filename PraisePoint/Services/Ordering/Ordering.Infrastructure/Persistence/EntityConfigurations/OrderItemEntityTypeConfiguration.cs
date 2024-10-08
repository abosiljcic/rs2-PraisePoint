﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence.EntityConfigurations
{
    public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).UseHiLo("orderitemseq");

            builder.Property<string>("ProductId")
                .HasColumnType("VARCHAR(24)")
                .HasColumnName("ProductId")
                .IsRequired();
        }
    }
}
