using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.UpdateSaleItem;

/// <summary>
/// Request model for updating a sale item.
/// </summary>
public class UpdateSaleItemRequest
{
    public Guid ProductId { get; set; }
    public decimal ProductUnitPrice { get; set; }
    public SaleItemStatus Status { get; set; }
}