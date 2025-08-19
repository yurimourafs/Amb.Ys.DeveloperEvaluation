using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Validator for UpdateSaleRequest
/// </summary>
public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    public UpdateSaleRequestValidator()
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

        RuleFor(s => s.Status)
            .NotEqual(SaleStatus.Unknown).WithMessage("Sale status cannot be Unknown.");
    }
}