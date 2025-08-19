using Ambev.DeveloperEvaluation.Application.SalesItems.CreateSaleItem;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.SalesItems.UpdateSaleItem
{
    public class UpdateSaleItemCommandValidator : AbstractValidator<UpdateSaleItemCommand>
    {
        public UpdateSaleItemCommandValidator()
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
