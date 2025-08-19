using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.SalesItems.CreateSaleItem
{
    public class CreateSaleItemCommandValidator : AbstractValidator<CreateSaleItemCommand>
    {
        public CreateSaleItemCommandValidator()
        {
            RuleFor(item => item.ProductId)
                .NotEqual(Guid.Empty).WithMessage("ProductId must be a valid UUID.");

            RuleFor(item => item.ProductUnitPrice)
                .GreaterThan(0).WithMessage("ProductUnitPrice must be greater than zero.");

            RuleFor(s => s.Status)
            .NotEqual(SaleItemStatus.Unknown).WithMessage("Sale status cannot be Unknown.");
        }
    }
}
