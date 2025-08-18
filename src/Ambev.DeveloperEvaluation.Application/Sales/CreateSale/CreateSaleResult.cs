namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Response returned after successfully creating a new sale.
/// </summary>
public class CreateSaleResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created sale.
    /// </summary>
    public Guid Id { get; set; }
}