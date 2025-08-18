using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleCommand
/// </summary>
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(s => s.SaleNumber)
            .NotEmpty().WithMessage("SaleNumber is required.")
            .MaximumLength(50).WithMessage("SaleNumber cannot be longer than 50 characters.");

        RuleFor(s => s.Date)
            .NotEmpty().WithMessage("Date is required.");

        RuleFor(s => s.CustomerId)
            .NotEqual(Guid.Empty).WithMessage("CustomerId must be a valid UUID.");

        RuleFor(s => s.BranchId)
            .NotEqual(Guid.Empty).WithMessage("BranchId must be a valid UUID.");

        RuleFor(s => s.TotalAmount)
            .GreaterThanOrEqualTo(0).WithMessage("TotalAmount must be non-negative.");

        RuleFor(s => s.Status)
            .NotEqual(SaleStatus.Unknown).WithMessage("Sale status cannot be Unknown.");

        RuleFor(sale => sale.Items)
            .NotEmpty().WithMessage("Sale must have at least one item.");
    }
}