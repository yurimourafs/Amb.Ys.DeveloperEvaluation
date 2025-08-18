using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItems;

/// <summary>
/// Request model for creating a new sale item.
/// </summary>
public class CreateSaleItemRequest
{
    public Guid ProductId { get; set; }
    public decimal ProductUnitPrice { get; set; }
    public SaleItemStatus Status { get; set; }
}