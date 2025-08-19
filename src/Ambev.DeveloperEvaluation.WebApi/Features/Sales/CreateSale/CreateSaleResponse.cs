using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.CreateSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.UpdateSaleItem;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Response model for a successfully created sale.
/// </summary>
public class CreateSaleResponse
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
    public List<CreateSaleItemResponse>? Items { get; set; }
}