using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        RuleFor(item => item.ProductId)
            .NotEmpty().WithMessage("ProductId must be greater than zero.");

        RuleFor(item => item.ProductUnitPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Product unit price must be non-negative.");

        RuleFor(sale => sale.Status)
            .IsInEnum().WithMessage("Status must be a valid SaleItemStatus enum value.");
    }
}