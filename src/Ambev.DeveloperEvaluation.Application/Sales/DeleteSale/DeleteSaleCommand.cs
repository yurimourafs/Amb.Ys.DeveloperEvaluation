using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Command for deleting a sale.
/// </summary>
public class DeleteSaleCommand : IRequest<DeleteSaleResult>
{
    public Guid Id { get; set; }
}