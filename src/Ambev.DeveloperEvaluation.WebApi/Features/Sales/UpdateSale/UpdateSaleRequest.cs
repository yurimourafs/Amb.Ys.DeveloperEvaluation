using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesItems;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.UpdateSaleItem;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Request model for updating a sale.
/// </summary>
public class UpdateSaleRequest
{
    public Guid Id { get; set; }
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public Guid CustomerId { get; set; }
    public Guid BranchId { get; set; }
    public SaleStatus Status { get; set; }

    public List<UpdateSaleItemRequest>? Items { get; set; }
}