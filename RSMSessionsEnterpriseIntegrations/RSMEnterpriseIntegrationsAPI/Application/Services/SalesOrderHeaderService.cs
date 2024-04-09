namespace RSMEnterpriseIntegrationsAPI.Application.Services
{
    using RSMEnterpriseIntegrationsAPI.Application.DTOs;
    using RSMEnterpriseIntegrationsAPI.Application.Exceptions;
    using RSMEnterpriseIntegrationsAPI.Application.Services;
    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces;
    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    public class SalesOrderHeaderService : ISalesOrderHeaderService
    {
        private readonly ISalesOrderHeaderRepository _salesRepository;
        public SalesOrderHeaderService(ISalesOrderHeaderRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }

        public async Task<IEnumerable<GetSalesOrderHeaderDto>> GetAll(int offset, int limit)
        {
            var salesOrderHeaders = await _salesRepository.GetAllSalesOrderHeaders(offset, limit);
            List<GetSalesOrderHeaderDto> salesOrderHeaderDtos = [];

            foreach (var salesOrderHeader in salesOrderHeaders)
            {
                GetSalesOrderHeaderDto dto = ConvertToSalesOrderHeaderDto(salesOrderHeader);
                salesOrderHeaderDtos.Add(dto);
            }

            return salesOrderHeaderDtos;

        }

        public async Task<GetSalesOrderHeaderDto?> GetSalesOrderHeaderById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("SalesOrderId is not valid");
            }

            var salesOrderHeader = await ValidateSalesOrderHeaderExistence(id);

            GetSalesOrderHeaderDto dto = ConvertToSalesOrderHeaderDto(salesOrderHeader);
            return dto;
        }

        public async Task<int> CreateSalesOrderHeader(CreateSalesOrderHeaderDto salesOrderHeaderDto)
        {
            
            if(salesOrderHeaderDto is null)
            {
                throw new BadRequestException("Sales Order Header info invalid");
            }

            SalesOrderHeader salesOrderHeader = new()
            {
                RevisionNumber = salesOrderHeaderDto.RevisionNumber,
                OrderDate = salesOrderHeaderDto.OrderDate,
                DueDate = salesOrderHeaderDto.DueDate,
                ShipDate = salesOrderHeaderDto.ShipDate,
                Status = salesOrderHeaderDto.Status,
                OnlineOrderFlag = salesOrderHeaderDto.OnlineOrderFlag,
                PurchaseOrderNumber = salesOrderHeaderDto.PurchaseOrderNumber,
                AccountNumber = salesOrderHeaderDto.AccountNumber,
                CustomerId = salesOrderHeaderDto.CustomerId,
                SalesPersonId = salesOrderHeaderDto.SalesPersonId,
                TerritoryId = salesOrderHeaderDto.TerritoryId,
                BillToAddressId = salesOrderHeaderDto.BillToAddressId,
                ShipToAddressId = salesOrderHeaderDto.ShipToAddressId,
                ShipMethodId = salesOrderHeaderDto.ShipMethodId,
                CreditCardId = salesOrderHeaderDto.CreditCardId,
                CreditCardApprovalCode = salesOrderHeaderDto.CreditCardApprovalCode,
                CurrencyRateId = salesOrderHeaderDto.CurrencyRateId,
                SubTotal = salesOrderHeaderDto.SubTotal,
                TaxAmt = salesOrderHeaderDto.TaxAmt,
                Freight = salesOrderHeaderDto.Freight,
                Comment = salesOrderHeaderDto.Comment
            };

            return await _salesRepository.CreateSalesOrderHeader(salesOrderHeader);
        }

        public async Task<int> UpdateSalesOrderHeader(UpdateSalesOrderHeaderDto salesOrderHeaderDto)
        {
            if(salesOrderHeaderDto is null)
            {
                throw new BadRequestException("Sales Order Header info is not valid");
            }

            var salesOrderHeader = await ValidateSalesOrderHeaderExistence(salesOrderHeaderDto.SalesOrderId);

            salesOrderHeader.SalesOrderId = salesOrderHeaderDto.SalesOrderId;
            salesOrderHeader.RevisionNumber = salesOrderHeaderDto.RevisionNumber;
            salesOrderHeader.OrderDate = salesOrderHeaderDto.OrderDate;
            salesOrderHeader.DueDate = salesOrderHeaderDto.DueDate;
            salesOrderHeader.ShipDate = salesOrderHeaderDto.ShipDate;
            salesOrderHeader.Status = salesOrderHeaderDto.Status;
            salesOrderHeader.OnlineOrderFlag = salesOrderHeaderDto.OnlineOrderFlag;
            salesOrderHeader.SalesOrderNumber = salesOrderHeaderDto.SalesOrderNumber is null ? salesOrderHeader.SalesOrderNumber : salesOrderHeaderDto.SalesOrderNumber;
            salesOrderHeader.PurchaseOrderNumber = salesOrderHeaderDto.PurchaseOrderNumber;
            salesOrderHeader.AccountNumber = salesOrderHeaderDto.AccountNumber;
            salesOrderHeader.CustomerId = salesOrderHeaderDto.CustomerId;
            salesOrderHeader.SalesPersonId = salesOrderHeaderDto.SalesPersonId;
            salesOrderHeader.TerritoryId = salesOrderHeaderDto.TerritoryId;
            salesOrderHeader.BillToAddressId = salesOrderHeaderDto.BillToAddressId;
            salesOrderHeader.ShipToAddressId = salesOrderHeaderDto.ShipToAddressId;
            salesOrderHeader.ShipMethodId = salesOrderHeaderDto.ShipMethodId;
            salesOrderHeader.CreditCardId = salesOrderHeaderDto.CreditCardId;
            salesOrderHeader.CreditCardApprovalCode = salesOrderHeaderDto.CreditCardApprovalCode;
            salesOrderHeader.CurrencyRateId = salesOrderHeaderDto.CurrencyRateId;
            salesOrderHeader.SubTotal = salesOrderHeaderDto.SubTotal;
            salesOrderHeader.TaxAmt = salesOrderHeaderDto.TaxAmt;
            salesOrderHeader.Freight = salesOrderHeaderDto.Freight;
            salesOrderHeader.Comment = salesOrderHeaderDto.Comment;

            return await _salesRepository.UpdateSalesOrderHeader(salesOrderHeader);

        }

        public async Task<int> DeleteSalesOrderHeader(int id)
        {
            if(id <= 0)
            {
                throw new BadRequestException("Id is not valid");
            }
            var salesOrderHeader = await ValidateSalesOrderHeaderExistence(id);
            return await _salesRepository.DeleteSalesOrderHeader(salesOrderHeader);
        }

        private async Task<SalesOrderHeader> ValidateSalesOrderHeaderExistence(int id)
        {
            var existingSale = await _salesRepository.GetSalesOrderHeaderById(id) 
                ?? throw new NotFoundException($"Sales Order with Id: {id} was not found.");

            return existingSale;
        }

        private static GetSalesOrderHeaderDto ConvertToSalesOrderHeaderDto(SalesOrderHeader salesOrderHeader)
        {
            return new GetSalesOrderHeaderDto{
                SalesOrderId = salesOrderHeader.SalesOrderId,
                RevisionNumber = salesOrderHeader.RevisionNumber,
                OrderDate = salesOrderHeader.OrderDate,
                DueDate = salesOrderHeader.DueDate,
                ShipDate = salesOrderHeader.ShipDate,
                Status = salesOrderHeader.Status,
                OnlineOrderFlag = salesOrderHeader.OnlineOrderFlag,
                SalesOrderNumber = salesOrderHeader.SalesOrderNumber,
                PurchaseOrderNumber = salesOrderHeader.PurchaseOrderNumber,
                AccountNumber = salesOrderHeader.AccountNumber,
                CustomerId = salesOrderHeader.CustomerId,
                SalesPersonId = salesOrderHeader.SalesPersonId,
                TerritoryId = salesOrderHeader.TerritoryId,
                BillToAddressId = salesOrderHeader.BillToAddressId,
                ShipToAddressId = salesOrderHeader.ShipToAddressId,
                ShipMethodId = salesOrderHeader.ShipMethodId,
                CreditCardId = salesOrderHeader.CreditCardId,
                CreditCardApprovalCode = salesOrderHeader.CreditCardApprovalCode,
                CurrencyRateId = salesOrderHeader.CurrencyRateId,
                SubTotal = salesOrderHeader.SubTotal,
                TaxAmt = salesOrderHeader.TaxAmt,
                Freight = salesOrderHeader.Freight,
                TotalDue = salesOrderHeader.TotalDue,
                Comment = salesOrderHeader.Comment
            };
        }
    }
}