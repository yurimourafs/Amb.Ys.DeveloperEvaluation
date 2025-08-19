using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents an item in a sale.
    /// </summary>
    public class SaleItem : BaseEntity
    {
        public Guid SaleId { get; set; }
        public Guid ProductId { get; set; }
        public decimal ProductUnitPrice { get; set; }
        public SaleItemStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Sale Sale { get; set; } = new();
    }
}
