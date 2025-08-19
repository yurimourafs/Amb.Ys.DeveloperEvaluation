using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator()
        {
            RuleFor(sale => sale.Items)
                .Must(items => items!.GroupBy(i => i.ProductId).All(g => g.Count() <= 20))
                .WithMessage("You cannot add more than 20 items of the same product to a sale.");
        }
    }
}
