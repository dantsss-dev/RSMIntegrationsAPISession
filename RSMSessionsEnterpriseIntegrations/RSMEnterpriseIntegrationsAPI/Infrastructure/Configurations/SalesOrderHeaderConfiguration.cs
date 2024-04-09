namespace RSMEnterpriseIntegrationsAPI.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    public class SalesOrderHeaderConfiguration : IEntityTypeConfiguration<SalesOrderHeader>
    {
        public void Configure(EntityTypeBuilder<SalesOrderHeader> builder)
        {
            builder.ToTable(nameof(SalesOrderHeader), "Sales", tb => tb.HasTrigger("uSalesOrderHeader"));

            builder.HasKey(e => e.SalesOrderId);

            builder.HasIndex(e => e.SalesOrderNumber, "AK_SalesOrderHeader_SalesOrderNumber").IsUnique();
            builder.HasIndex(e => e.Rowguid, "AK_SalesOrderHeader_rowguid").IsUnique();

            builder.Property(e => e.SalesOrderId).HasColumnName("SalesOrderID");
            builder.Property(e => e.AccountNumber).HasMaxLength(15);
            builder.Property(e => e.BillToAddressId).HasColumnName("BillToAddressID");
            builder.Property(e => e.Comment).HasMaxLength(128);
            builder.Property(e => e.CreditCardApprovalCode).HasMaxLength(15).IsUnicode(false);
            builder.Property(e => e.CreditCardId).HasColumnName("CreditCardID");
            builder.Property(e => e.CurrencyRateId).HasColumnName("CurrencyRateID");
            builder.Property(e => e.CustomerId).HasColumnName("CustomerID");
            builder.Property(e => e.DueDate).HasColumnType("datetime");
            builder.Property(e => e.Freight).HasColumnType("money");
            builder.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())").HasColumnType("datetime");
            builder.Property(e => e.OnlineOrderFlag).HasDefaultValue(true);
            builder.Property(e => e.OrderDate).HasDefaultValueSql("(getdate())").HasColumnType("datetime");
            builder.Property(e => e.PurchaseOrderNumber).HasMaxLength(25);
            builder.Property(e => e.Rowguid).HasDefaultValueSql("(newid())").HasColumnName("rowguid");
            builder.Property(e => e.SalesOrderNumber).HasMaxLength(25).HasComputedColumnSql("(isnull(N'SO'+CONVERT([nvarchar](23),[SalesOrderID]),N'*** ERROR ***'))", false);
            builder.Property(e => e.SalesPersonId).HasColumnName("SalesPersonID");
            builder.Property(e => e.ShipDate).HasColumnType("datetime");
            builder.Property(e => e.ShipMethodId).HasColumnName("ShipMethodID");
            builder.Property(e => e.ShipToAddressId).HasColumnName("ShipToAddressID");
            builder.Property(e => e.Status).HasDefaultValue((byte)1);
            builder.Property(e => e.SubTotal).HasColumnType("money");
            builder.Property(e => e.TaxAmt).HasColumnType("money");
            builder.Property(e => e.TerritoryId).HasColumnName("TerritoryID");
            builder.Property(e => e.TotalDue).HasComputedColumnSql("(isnull(([SubTotal]+[TaxAmt])+[Freight],(0)))", false).HasColumnType("money");
        } 
    }
}