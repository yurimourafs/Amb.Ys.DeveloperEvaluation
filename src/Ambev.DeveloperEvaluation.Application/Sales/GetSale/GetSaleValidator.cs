using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Validator for GetSaleCommand
    /// </summary>
    public class GetSaleValidator : AbstractValidator<GetSaleQuery>
    {
        public GetSaleValidator()
        {
            RuleFor(cmd => cmd.Id)
                .NotEmpty().WithMessage("Sale ID is required.");
        }
    }
}
