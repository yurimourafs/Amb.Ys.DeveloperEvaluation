using Ambev.DeveloperEvaluation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Response model for GetSale operation
    /// </summary>
    public class GetSaleResult
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public Guid CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid BranchId { get; set; }
        public SaleStatus Status { get; set; }
    }
}
