using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.CreateSaleItem
{
    public class CreateSaleItemResponse
    {
        public Guid ProductId { get; set; }
        public decimal ProductUnitPrice { get; set; }
        public SaleItemStatus Status { get; set; }
    }
}
