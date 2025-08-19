using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;
using FluentValidation.Results;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a sale transaction in the system.
    /// </summary>
    public class Sale : BaseEntity
    {
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public Guid CustomerId { get; set; }
        public decimal? Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountWithDiscount { get; set; }
        public Guid BranchId { get; set; }
        public SaleStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual List<SaleItem>? Items { get; set; }

        public void ApplyDiscount()
        {
            if (Items == null || Items.Count == 0)
            {
                Discount = 0;
                TotalAmount = 0;
                TotalAmountWithDiscount = 0;
                return;
            }

            // Agrupa por ProductId e soma as quantidades
            var productQuantities = Items
                .GroupBy(i => i.ProductId)
                .Select(g => new { ProductId = g.Key, Count = g.Count(), Items = g.ToList() })
                .ToList();

            decimal totalAmount = Items.Sum(i => i.ProductUnitPrice);

            // Determina o maior tier de desconto aplicável
            int maxIdentical = productQuantities.Max(g => g.Count);
            int totalItems = Items.Count;

            decimal discountPercent = 0;

            if (maxIdentical >= 4)
                discountPercent = 0.10m;

            if (totalItems >= 10 && totalItems <= 20)
                discountPercent = Math.Max(discountPercent, 0.20m);

            Discount = discountPercent * 100; // em porcentagem
            TotalAmount = totalAmount;
            TotalAmountWithDiscount = totalAmount * (1 - discountPercent);
        }

        public void Update()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        public ValidationResultDetail Validate()
        {
            var validator = new SaleValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
