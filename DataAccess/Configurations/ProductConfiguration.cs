using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.HasIndex(p => p.Name)
                   .IsUnique();
            builder.HasMany(p => p.ProductOrders)
                   .WithOne(po => po.Product)
                   .HasForeignKey(po => po.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
