namespace RSMEnterpriseIntegrationsAPI.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product), "Production");

            builder.HasKey(p => p.ProductId);

            builder.HasIndex(e => e.Name, "AK_Product_Name").IsUnique();
            builder.HasIndex(e => e.ProductNumber, "AK_Product_ProductNumber").IsUnique();
            builder.HasIndex(e => e.Rowguid, "AK_Product_rowguid").IsUnique();

            builder.Property(e => e.ProductId).HasColumnName("ProductID");
            builder.Property(e => e.Class).HasMaxLength(2).IsFixedLength();
            builder.Property(e => e.Color).HasMaxLength(15);
            builder.Property(e => e.DiscontinuedDate).HasColumnType("datetime");
            builder.Property(e => e.FinishedGoodsFlag).HasDefaultValue(true);
            builder.Property(e => e.ListPrice).HasColumnType("money");
            builder.Property(e => e.MakeFlag).HasDefaultValue(true);
            builder.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())").HasColumnType("datetime");
            builder.Property(e => e.Name).HasMaxLength(50);
            builder.Property(e => e.ProductLine).HasMaxLength(2).IsFixedLength();
            builder.Property(e => e.ProductModelId).HasColumnName("ProductModelID");
            builder.Property(e => e.ProductNumber).HasMaxLength(25);
            builder.Property(e => e.ProductSubcategoryId).HasColumnName("ProductSubcategoryID");
            builder.Property(e => e.Rowguid).HasDefaultValueSql("(newid())").HasColumnName("rowguid");
            builder.Property(e => e.SellEndDate).HasColumnType("datetime");
            builder.Property(e => e.SellStartDate).HasColumnType("datetime");
            builder.Property(e => e.Size).HasMaxLength(5);
            builder.Property(e => e.SizeUnitMeasureCode).HasMaxLength(3).IsFixedLength();
            builder.Property(e => e.StandardCost).HasColumnType("money");
            builder.Property(e => e.Style).HasMaxLength(2).IsFixedLength();
            builder.Property(e => e.Weight).HasColumnType("decimal(8,2)");
            builder.Property(e => e.WeightUnitMeasureCode).HasMaxLength(3).IsFixedLength();
            
        }
    }
}
