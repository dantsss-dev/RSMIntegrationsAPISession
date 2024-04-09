namespace RSMEnterpriseIntegrationsAPI.Domain.Interfaces
{
    using RSMEnterpriseIntegrationsAPI.Application.DTOs;

    public interface IProductService
    {
        Task<GetProductDto?> GetProductById(int id);
        Task<IEnumerable<GetProductDto>> GetAll();
        Task<int> CreateProduct(CreateProductDto productDto);
        Task<int> UpdateProduct(UpdateProductDto productDto);
        Task<int> DeleteProduct(int id);
        
    }
}