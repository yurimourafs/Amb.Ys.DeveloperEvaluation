using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.UpdateSaleItem;

/// <summary>
/// Validator for UpdateSaleItemRequest
/// </summary>
public class UpdateSaleItemRequestValidator : AbstractValidator<UpdateSaleItemRequest>
{
    public UpdateSaleItemRequestValidator()
    {
        RuleFor(i => i.ProductId)
            .NotEqual(Guid.Empty).WithMessage("ProductId must be a valid UUID.");

        RuleFor(i => i.ProductUnitPrice)
            .GreaterThanOrEqualTo(0).WithMessage("ProductUnitPrice must be non-negative.");

        RuleFor(i => i.Status)
            .NotEqual(SaleItemStatus.Unknown).WithMessage("Sale item status cannot be Unknown.");
    }
}