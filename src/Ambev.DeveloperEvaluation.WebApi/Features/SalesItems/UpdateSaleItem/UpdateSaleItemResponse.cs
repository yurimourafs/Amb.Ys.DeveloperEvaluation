using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.UpdateSaleItem
{
    public class UpdateSaleItemResponse
    {
        public Guid ProductId { get; set; }
        public decimal ProductUnitPrice { get; set; }
        public SaleItemStatus Status { get; set; }
    }
}
