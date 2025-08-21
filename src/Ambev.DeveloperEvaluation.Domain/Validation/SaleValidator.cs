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
                .NotEmpty().WithMessage("SaleNumber is required.")
                .MaximumLength(50).WithMessage("SaleNumber cannot be longer than 50 characters.");

            RuleFor(sale => sale.Date)
                .NotEmpty().WithMessage("Date is required.")
                .LessThan(DateTime.Now).WithMessage("Sale date is invalid.");

            RuleFor(sale => sale.CustomerId)
                .NotEqual(Guid.Empty).WithMessage("CustomerId must be a valid UUID.");

            RuleFor(sale => sale.BranchId)
                .NotEqual(Guid.Empty).WithMessage("BranchId must be a valid UUID.");

            RuleFor(sale => sale.TotalAmount)
                .GreaterThanOrEqualTo(0).WithMessage("TotalAmount must be non-negative.");

            RuleFor(sale => sale.TotalAmountWithDiscount)
                .GreaterThanOrEqualTo(0).WithMessage("TotalAmountWithDiscount must be non-negative.");

            RuleFor(sale => sale.Status)
                .NotEqual(SaleStatus.Unknown).WithMessage("Sale status cannot be Unknown.");

            RuleFor(sale => sale.Items)
                .Must(items => items!.GroupBy(i => i.ProductId).All(g => g.Count() <= 20))
                .WithMessage("You cannot add more than 20 items of the same product to a sale.");
        }
    }
}
