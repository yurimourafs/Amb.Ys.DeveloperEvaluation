using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data for Sale entities using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class SaleTestData
{
    private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
        .RuleFor(i => i.ProductId, f => f.Random.Guid())
        .RuleFor(i => i.ProductUnitPrice, f => f.Random.Decimal(10, 100))
        .RuleFor(i => i.Status, f => f.PickRandom<SaleItemStatus>());

    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(s => s.SaleNumber, f => f.Random.AlphaNumeric(10))
        .RuleFor(s => s.Date, f => f.Date.Recent())
        .RuleFor(s => s.CustomerId, f => f.Random.Guid())
        .RuleFor(s => s.BranchId, f => f.Random.Guid())
        .RuleFor(s => s.Status, f => f.PickRandom<SaleStatus>())
        .RuleFor(s => s.Items, f => new List<SaleItem>());

    /// <summary>
    /// Generates a valid Sale entity with a specified number of items.
    /// </summary>
    /// <param name="totalItems">Total number of items in the sale.</param>
    /// <param name="identicalProductId">If provided, all items will have the same ProductId (for identical items discount tier).</param>
    /// <returns>A valid Sale entity with randomized data and items.</returns>
    public static Sale GenerateSaleWithItems(int totalItems, Guid? identicalProductId = null)
    {
        var sale = SaleFaker.Generate();
        sale.Items = new List<SaleItem>();

        for (int i = 0; i < totalItems; i++)
        {
            var item = SaleItemFaker.Generate();
            if (identicalProductId.HasValue)
                item.ProductId = identicalProductId.Value;
            sale.Items.Add(item);
        }

        // Calculate total amounts before discount
        sale.ApplyDiscount();

        return sale;
    }

    /// <summary>
    /// Generates a Sale with 4 identical items (for 10% discount tier).
    /// </summary>
    public static Sale GenerateSaleWithFourIdenticalItems()
    {
        var productId = Guid.NewGuid();
        return GenerateSaleWithItems(4, productId);
    }

    /// <summary>
    /// Generates a Sale with 10 items (for 20% discount tier).
    /// </summary>
    public static Sale GenerateSaleWithTenItems()
    {
        return GenerateSaleWithItems(10);
    }

    /// <summary>
    /// Generates a Sale with less than 4 items (no discount).
    /// </summary>
    public static Sale GenerateSaleWithNoDiscount()
    {
        return GenerateSaleWithItems(3);
    }
}