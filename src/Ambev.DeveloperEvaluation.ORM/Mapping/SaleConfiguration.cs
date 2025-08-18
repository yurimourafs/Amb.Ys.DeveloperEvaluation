using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(s => s.SaleNumber).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Date).IsRequired();
            builder.Property(s => s.CustomerId).IsRequired().HasColumnType("uuid");
            builder.Property(s => s.BranchId).IsRequired().HasColumnType("uuid");
            builder.Property(s => s.Discount).HasColumnType("numeric(18,2)");
            builder.Property(s => s.TotalAmount).IsRequired().HasColumnType("numeric(18,2)");
            builder.Property(s => s.TotalAmountWithDiscount).IsRequired().HasColumnType("numeric(18,2)");

            builder.HasIndex(s => new { s.SaleNumber, s.BranchId })
                .IsUnique()
                .HasDatabaseName("IX_Unique_Sales_SaleNumber_BranchId");

            builder.Property(s => s.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(s => s.CreatedAt).IsRequired();
            builder.Property(s => s.UpdatedAt);
        }
    }
}
