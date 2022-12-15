using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Config
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.HasOne(p => p.Product).WithMany()
            .HasForeignKey(p => p.ProductId);
             builder.HasOne(p => p.Order).WithMany()
            .HasForeignKey(p => p.OrderId);
        }
    }
}