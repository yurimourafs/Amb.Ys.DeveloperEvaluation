using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Sale entity class.
/// Tests cover discount calculation, update, and delete scenarios.
/// </summary>
public class SaleTests
{
    [Fact(DisplayName = "CreateSale should have no discount when less than 4 items")]
    public void Given_SaleWithLessThanFourItems_When_ApplyDiscount_Then_NoDiscountApplied()
    {
        // Arrange
        var sale = SaleTestData.GenerateSaleWithNoDiscount();

        // Act
        sale.ApplyDiscount();

        // Assert
        Assert.Equal(0, sale.Discount);
        Assert.Equal(sale.TotalAmount, sale.TotalAmountWithDiscount);
    }

    [Fact(DisplayName = "CreateSale should apply 10% discount for 4+ identical items")]
    public void Given_SaleWithFourIdenticalItems_When_ApplyDiscount_Then_TenPercentDiscountApplied()
    {
        // Arrange
        var sale = SaleTestData.GenerateSaleWithFourIdenticalItems();
        var expectedDiscount = 0.10m;

        // Act
        sale.ApplyDiscount();

        // Assert
        Assert.Equal(10, sale.Discount); // Discount in percent
        Assert.Equal(sale.TotalAmount * (1 - expectedDiscount), sale.TotalAmountWithDiscount, 2);
    }

    [Fact(DisplayName = "CreateSale should apply 20% discount for 10-20 items")]
    public void Given_SaleWithTenItems_When_ApplyDiscount_Then_TwentyPercentDiscountApplied()
    {
        // Arrange
        var sale = SaleTestData.GenerateSaleWithTenItems();
        var expectedDiscount = 0.20m;

        // Act
        sale.ApplyDiscount();

        // Assert
        Assert.Equal(20, sale.Discount); // Discount in percent
        Assert.Equal(sale.TotalAmount * (1 - expectedDiscount), sale.TotalAmountWithDiscount, 2);
    }
}