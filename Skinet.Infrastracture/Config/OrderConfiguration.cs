using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skinet.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skinet.Infrastracture.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(
            i => i.ItemOrdered,
            io => { io.WithOwner(); });
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
        }
    }
}
