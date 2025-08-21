using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

public class SalesControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public SalesControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateSale_ShouldReturn201AndSaleData()
    {
        var request = new CreateSaleRequest
        {
            SaleNumber = "SALE123",
            Date = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            Status = SaleStatus.Active
        };

        var response = await _client.PostAsJsonAsync("/api/sales", request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("Sale created successfully");
    }

    [Fact]
    public async Task GetSale_ShouldReturn200AndSaleData()
    {
        // First, create a sale
        var createRequest = new CreateSaleRequest
        {
            SaleNumber = "SALE_TO_GET",
            Date = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            Status = SaleStatus.Active
        };
        var createResponse = await _client.PostAsJsonAsync("/api/sales", createRequest);
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        var createContent = await createResponse.Content.ReadAsStringAsync();
        var createdSale = JsonDocument.Parse(createContent).RootElement.GetProperty("data");
        var saleId = createdSale.GetProperty("id").GetGuid();

        // Get the sale by id
        var getResponse = await _client.GetAsync($"/api/sales/{saleId}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task UpdateSale_ShouldReturn200AndUpdatedSale()
    {
        // First, create a sale
        var createRequest = new CreateSaleRequest
        {
            SaleNumber = "SALE_TO_UPDATE",
            Date = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            Status = SaleStatus.Active
        };
        var createResponse = await _client.PostAsJsonAsync("/api/sales", createRequest);
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        var createContent = await createResponse.Content.ReadAsStringAsync();
        var createdSale = JsonDocument.Parse(createContent).RootElement.GetProperty("data");
        var saleId = createdSale.GetProperty("id").GetGuid();

        // Prepare update
        var updateRequest = new UpdateSaleRequest
        {
            Id = saleId,
            SaleNumber = "SALE_UPDATED",
            Date = DateTime.UtcNow,
            CustomerId = createRequest.CustomerId,
            BranchId = createRequest.BranchId,
            Status = SaleStatus.Active
        };

        var updateResponse = await _client.PutAsJsonAsync($"/api/sales/{saleId}", updateRequest);
        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task DeleteSale_ShouldReturn200AndSuccessMessage()
    {
        // First, create a sale
        var createRequest = new CreateSaleRequest
        {
            SaleNumber = "SALE_TO_DELETE",
            Date = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            Status = SaleStatus.Active
        };
        var createResponse = await _client.PostAsJsonAsync("/api/sales", createRequest);
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        var createContent = await createResponse.Content.ReadAsStringAsync();
        var createdSale = JsonDocument.Parse(createContent).RootElement.GetProperty("data");
        var saleId = createdSale.GetProperty("id").GetGuid();

        // Delete
        var deleteResponse = await _client.DeleteAsync($"/api/sales/{saleId}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task ListSales_ShouldReturn200AndPaginatedSales()
    {
        // Ensure at least one sale exists
        var createRequest = new CreateSaleRequest
        {
            SaleNumber = "SALE_LIST",
            Date = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            Status = SaleStatus.Active
        };
        await _client.PostAsJsonAsync("/api/sales", createRequest);

        var response = await _client.GetAsync("/api/sales?page=1&size=10");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("items");
        content.Should().Contain("currentPage");
        content.Should().Contain("totalPages");
    }
}