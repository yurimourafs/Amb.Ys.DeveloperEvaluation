using Ambev.DeveloperEvaluation.Application.SalesItems.UpdateSaleItem;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Response returned after successfully updating a sale.
/// </summary>
public class UpdateSaleResult
{
    public Guid Id { get; set; }
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public Guid CustomerId { get; set; }
    public decimal? Discount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal TotalAmountWithDiscount { get; set; }
    public Guid BranchId { get; set; }
    public SaleStatus Status { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<UpdateSaleItemResult>? Items { get; set; }
}