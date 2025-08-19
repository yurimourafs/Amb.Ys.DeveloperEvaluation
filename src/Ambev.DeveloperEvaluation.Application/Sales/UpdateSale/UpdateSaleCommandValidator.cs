using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Validator for UpdateSaleCommand
/// </summary>
public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
{
    public UpdateSaleCommandValidator()
    {
        RuleFor(s => s.Id)
            .NotEqual(Guid.Empty).WithMessage("Id is required.");

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
    }
}