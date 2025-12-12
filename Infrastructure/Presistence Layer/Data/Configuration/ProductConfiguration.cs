using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entitys.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence_Layer.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
           builder.HasOne(p => p.ProductBrand)
               .WithMany()
               .HasForeignKey(p => p.BrandId);

            builder.HasOne(p => p.ProductType)
                .WithMany()
                .HasForeignKey(p => p.TypeId);

            builder.Property(p => p.Name).HasColumnName("Name").HasColumnType("nvarchar(200)");
            builder.Property(p => p.Description).HasColumnName("Description").HasColumnType("nvarchar(500)");
            builder.Property(p=>p.Price).HasColumnType("decimal(10,2)");
        }
    }
}
