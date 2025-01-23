using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NiceStore.Catalog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceStore.Catalog.Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);  
            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(p => p.Description)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(p => p.Image)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.OwnsOne(p => p.Dymensions, dymensions =>
            {
                dymensions.Property(d => d.Height)
                    .HasColumnName("Height")
                    .HasColumnType("int");

                dymensions.Property(d => d.Width)
                    .HasColumnName("Width")
                    .HasColumnType("int");

                dymensions.Property(d => d.Depth)
                    .HasColumnName("Depth")
                    .HasColumnType("int");
            });

            builder.ToTable("Products");
        }
    }
}
