using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesItems;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Integration.Controllers.TestData
{
    internal static class SaleControllerData
    {
        private static readonly Faker<CreateSaleItemRequest> SaleItemFaker = new Faker<CreateSaleItemRequest>()
        .RuleFor(i => i.ProductId, f => f.Random.Guid())
        .RuleFor(i => i.ProductUnitPrice, f => f.Random.Decimal(10, 100))
        .RuleFor(i => i.Status, f => SaleItemStatus.Active);

        private static readonly Faker<CreateSaleRequest> SaleFaker = new Faker<CreateSaleRequest>()
            .RuleFor(s => s.SaleNumber, f => f.Random.AlphaNumeric(10))
            .RuleFor(s => s.Date, f => new DateTime(f.Date.Recent().Ticks, DateTimeKind.Utc))
            .RuleFor(s => s.CustomerId, f => f.Random.Guid())
            .RuleFor(s => s.BranchId, f => f.Random.Guid())
            .RuleFor(s => s.Status, f => SaleStatus.Active)
            .RuleFor(s => s.Items, f => new List<CreateSaleItemRequest>());

        public static CreateSaleRequest GenerateSaleWithItems(int totalItems, Guid? identicalProductId = null)
        {
            var sale = SaleFaker.Generate();
            sale.Items = new List<CreateSaleItemRequest>();

            for (int i = 0; i < totalItems; i++)
            {
                var item = SaleItemFaker.Generate();
                if (identicalProductId.HasValue)
                    item.ProductId = identicalProductId.Value;
                sale.Items.Add(item);
            }

            return sale;
        }
    }
}
