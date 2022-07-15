using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skinet.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skinet.Infrastracture.Config
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(p=>p.Price).HasColumnType("decimal(18,2)");
        }
    }
}
