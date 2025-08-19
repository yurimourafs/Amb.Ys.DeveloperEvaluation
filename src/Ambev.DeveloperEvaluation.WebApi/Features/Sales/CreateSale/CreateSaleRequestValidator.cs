using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleRequest
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
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

        RuleFor(s => s.Status)
            .NotEqual(SaleStatus.Unknown).WithMessage("Sale status cannot be Unknown.");
    }
}