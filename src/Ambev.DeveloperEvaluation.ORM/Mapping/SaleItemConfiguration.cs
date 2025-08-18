using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(si => si.Id);
            builder.Property(si => si.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(si => si.SaleId).IsRequired();
            builder.Property(si => si.ProductId).HasColumnType("uuid").IsRequired();
            builder.Property(si => si.ProductUnitPrice).IsRequired().HasColumnType("numeric(18,2)");

            builder.Property(si => si.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(si => si.CreatedAt).IsRequired();
            builder.Property(si => si.UpdatedAt);

            builder.HasOne(si => si.Sale)
                .WithMany(s => s.Items)
                .HasForeignKey(si => si.SaleId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
