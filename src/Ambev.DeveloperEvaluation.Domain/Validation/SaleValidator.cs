using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator()
        {
            RuleFor(sale => sale.SaleNumber)
                .NotEmpty()
                .MaximumLength(50).WithMessage("Sale number cannot be longer than 50 characters.");

            RuleFor(sale => sale.Date)
                .NotEmpty().WithMessage("Sale date is required.");

            RuleFor(sale => sale.CustomerId)
                .NotEqual(Guid.Empty).WithMessage("CustomerId must be a valid UUID.");

            RuleFor(sale => sale.BranchId)
                .NotEqual(Guid.Empty).WithMessage("BranchId must be a valid UUID.");

            RuleFor(sale => sale.Status)
                .IsInEnum().WithMessage("Status must be a valid SaleStatus enum value.");

            RuleFor(sale => sale.Items)
                .NotEmpty().WithMessage("Sale must have at least one item.");

            RuleForEach(sale => sale.Items).SetValidator(new SaleItemValidator());
        }
    }
}
