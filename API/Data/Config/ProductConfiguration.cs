using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Config 
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
           builder.Property(p => p.Id).IsRequired();
           builder.HasOne(p => p.Category).WithMany()
           .HasForeignKey(p => p.CategoryId);
        }
    }
}