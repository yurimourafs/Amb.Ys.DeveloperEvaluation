using Ambev.DeveloperEvaluation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.SalesItems.CreateSaleItem
{
    public class CreateSaleItemResult
    {
        public Guid ProductId { get; set; }
        public decimal ProductUnitPrice { get; set; }
        public SaleItemStatus Status { get; set; }
    }
}
