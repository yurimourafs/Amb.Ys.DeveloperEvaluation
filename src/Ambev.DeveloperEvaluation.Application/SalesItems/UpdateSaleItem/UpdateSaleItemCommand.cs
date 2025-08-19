using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.SalesItems.UpdateSaleItem;

/// <summary>
/// Command for updating a sale item.
/// </summary>
public class UpdateSaleItemCommand
{
    public Guid ProductId { get; set; }
    public decimal ProductUnitPrice { get; set; }
    public SaleItemStatus Status { get; set; }
}