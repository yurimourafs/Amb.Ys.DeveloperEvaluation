using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Request model for creating a new sale.
/// </summary>
public class CreateSaleRequest
{
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public Guid CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public Guid BranchId { get; set; }
    public SaleStatus Status { get; set; }
}