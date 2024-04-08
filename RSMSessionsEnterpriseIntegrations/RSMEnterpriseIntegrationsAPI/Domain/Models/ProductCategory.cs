namespace RSMEnterpriseIntegrationsAPI.Domain.Models
{
    public class ProductCategory
    {
        public int ProductCategoryId { get; set; }
        public string? Name { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}