namespace RSMEnterpriseIntegrationsAPI.Application.Services
{
    using RSMEnterpriseIntegrationsAPI.Application.DTOs;
    using RSMEnterpriseIntegrationsAPI.Application.Exceptions;
    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces;
    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository repository)
        {
            _productRepository = repository;
        }

        public async Task<IEnumerable<GetProductDto>> GetAll()
        {
            var products = await _productRepository.GetAllProducts();
            List<GetProductDto> productsDto = [];

            foreach (var product in products)
            {
                GetProductDto dto = new()
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    ProductNumber = product.ProductNumber,
                    MakeFlag = product.MakeFlag,
                    FinishedGoodsFlag = product.FinishedGoodsFlag,
                    Color = product.Color,
                    SafetyStockLevel = product.SafetyStockLevel,
                    ReorderPoint = product.ReorderPoint,
                    StandardCost = product.StandardCost,
                    ListPrice = product.ListPrice,
                    Size = product.Size,
                    SizeUnitMeasureCode = product.SizeUnitMeasureCode,
                    WeightUnitMeasureCode = product.WeightUnitMeasureCode,
                    Weight = product.Weight,
                    DaysToManufacture = product.DaysToManufacture,
                    ProductLine = product.ProductLine,
                    Class = product.Class,
                    Style = product.Style,
                    ProductSubcategoryId = product.ProductSubcategoryId,
                    ProductModelId = product.ProductModelId,
                    SellStartDate = product.SellStartDate,
                    SellEndDate = product.SellEndDate,
                    DiscontinuedDate = product.DiscontinuedDate,
                };
                productsDto.Add(dto);
            }

            return productsDto;
        }

        public async Task<GetProductDto?> GetProductById(int id)
        {
            if(id <= 0)
            {
                throw new BadRequestException("ProductId is not valid");
            }

            var product = await ValidateProductExistence(id);

            GetProductDto dto = new()
            {
                ProductId = product.ProductId,
                Name = product.Name,
                ProductNumber = product.ProductNumber,
                MakeFlag = product.MakeFlag,
                FinishedGoodsFlag = product.FinishedGoodsFlag,
                Color = product.Color,
                SafetyStockLevel = product.SafetyStockLevel,
                ReorderPoint = product.ReorderPoint,
                StandardCost = product.StandardCost,
                ListPrice = product.ListPrice,
                Size = product.Size,
                SizeUnitMeasureCode = product.SizeUnitMeasureCode,
                WeightUnitMeasureCode = product.WeightUnitMeasureCode,
                Weight = product.Weight,
                DaysToManufacture = product.DaysToManufacture,
                ProductLine = product.ProductLine,
                Class = product.Class,
                Style = product.Style,
                ProductSubcategoryId = product.ProductSubcategoryId,
                ProductModelId = product.ProductModelId,
                SellStartDate = product.SellStartDate,
                SellEndDate = product.SellEndDate,
                DiscontinuedDate = product.DiscontinuedDate,
            };
            return dto;
        }

        public async Task<int> CreateProduct(CreateProductDto productDto)
        {
            if(productDto is null
               || string.IsNullOrWhiteSpace(productDto.Name))
            {
                throw new BadRequestException("Product info is not valid");
            }

            Product product = new()
            {
                Name = productDto.Name,
                ProductNumber = productDto.ProductNumber,
                MakeFlag = productDto.MakeFlag,
                FinishedGoodsFlag = productDto.FinishedGoodsFlag,
                Color = productDto.Color,
                SafetyStockLevel = productDto.SafetyStockLevel,
                ReorderPoint = productDto.ReorderPoint,
                StandardCost = productDto.StandardCost,
                ListPrice = productDto.ListPrice,
                Size = productDto.Size,
                SizeUnitMeasureCode = productDto.SizeUnitMeasureCode,
                WeightUnitMeasureCode = productDto.WeightUnitMeasureCode,
                Weight = productDto.Weight,
                DaysToManufacture = productDto.DaysToManufacture,
                ProductLine = productDto.ProductLine,
                Class = productDto.Class,
                Style = productDto.Style,
                ProductSubcategoryId = productDto.ProductSubcategoryId,
                ProductModelId = productDto.ProductModelId,
                SellStartDate = productDto.SellStartDate,
                SellEndDate = productDto.SellEndDate,
                DiscontinuedDate = productDto.DiscontinuedDate,
            };

            return await _productRepository.CreateProduct(product);
        }

        public async Task<int> UpdateProduct(UpdateProductDto productDto)
        {
            if (productDto is null)
            {
                throw new BadRequestException("Product info is not valid");
            }

            var product = await ValidateProductExistence(productDto.ProductId);

            product.Name = string.IsNullOrWhiteSpace(productDto.Name) ? product.Name : productDto.Name;
            product.ProductNumber = string.IsNullOrWhiteSpace(productDto.ProductNumber) ? product.ProductNumber : productDto.ProductNumber;
            product.MakeFlag = productDto.MakeFlag ;
            product.FinishedGoodsFlag = productDto.FinishedGoodsFlag;
            product.Color = productDto.Color;
            product.SafetyStockLevel = productDto.SafetyStockLevel;
            product.ReorderPoint = productDto.ReorderPoint;
            product.StandardCost = productDto.StandardCost;
            product.ListPrice = productDto.ListPrice;
            product.Size = productDto.Size;
            product.SizeUnitMeasureCode = productDto.SizeUnitMeasureCode;
            product.WeightUnitMeasureCode = productDto.WeightUnitMeasureCode;
            product.Weight = productDto.Weight;
            product.DaysToManufacture = productDto.DaysToManufacture;
            product.ProductLine = productDto.ProductLine;
            product.Class = productDto.Class;
            product.Style = productDto.Style;
            product.ProductSubcategoryId = productDto.ProductSubcategoryId;
            product.ProductModelId = productDto.ProductModelId;

            product.SellStartDate = productDto.SellStartDate; 
            product.SellEndDate = productDto.SellEndDate;
            product.DiscontinuedDate = productDto.DiscontinuedDate;


            return await _productRepository.UpdateProduct(product);
        }

        public async Task<int> DeleteProduct(int id)
        {
            if(id <= 0)
            {
                throw new BadRequestException("Id is not valid");
            }
            var product = await ValidateProductExistence(id);
            return await _productRepository.DeleteProduct(product);
        }

        private async Task<Product> ValidateProductExistence(int id)
        {
            var existingProduct = await _productRepository.GetProductById(id) 
                ?? throw new NotFoundException($"Product with Id: {id} was not found.");

            return existingProduct;
        }
    }
}