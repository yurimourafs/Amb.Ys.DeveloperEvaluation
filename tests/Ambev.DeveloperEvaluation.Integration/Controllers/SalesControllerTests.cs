using Ambev.DeveloperEvaluation.WebApi;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.UpdateSaleItem;
using Ambev.DeveloperEvaluation.Integration.Controllers.TestData;

public class SalesControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public SalesControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    private async Task AuthenticateAsync()
    {
        var loginRequest = new
        {
            email = "admin@email.com",
            password = "Admin@123"
        };

        var response = await _client.PostAsJsonAsync("/api/auth", loginRequest);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(content);
        var token = json.RootElement.GetProperty("data").GetProperty("data").GetProperty("token").GetString();

        _client.DefaultRequestHeaders.Remove("Authorization");
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
    }

    [Fact]
    public async Task CreateSale_ShouldReturn201AndSaleData()
    {
        await AuthenticateAsync();

        var requestObj = SaleControllerData.GenerateSaleWithItems(2);

        var response = await _client.PostAsJsonAsync("/api/sales", requestObj);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task GetSale_ShouldReturn200AndSaleData()
    {
        await AuthenticateAsync();

        // First, create a sale
        var createRequest = SaleControllerData.GenerateSaleWithItems(2);

        var createResponse = await _client.PostAsJsonAsync("/api/sales", createRequest);
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var createContent = await createResponse.Content.ReadAsStringAsync();
        var createdSale = JsonDocument.Parse(createContent).RootElement.GetProperty("data").GetProperty("data");
        var saleId = createdSale.GetProperty("id").GetGuid();

        // Get the sale by id
        var getResponse = await _client.GetAsync($"/api/sales/{saleId}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task UpdateSale_ShouldReturn200AndUpdatedSale()
    {
        await AuthenticateAsync();

        // First, create a sale
        var createRequest = SaleControllerData.GenerateSaleWithItems(2);

        var createResponse = await _client.PostAsJsonAsync("/api/sales", createRequest);
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var createContent = await createResponse.Content.ReadAsStringAsync();
        var createdSale = JsonDocument.Parse(createContent).RootElement.GetProperty("data").GetProperty("data");
        var saleId = createdSale.GetProperty("id").GetGuid();

        // Prepare update
        var updateRequest = new UpdateSaleRequest
        {
            Id = saleId,
            SaleNumber = createRequest.SaleNumber,
            Date = createRequest.Date,
            CustomerId = createRequest.CustomerId,
            BranchId = createRequest.BranchId,
            Status = SaleStatus.Canceled,
            Items = createRequest.Items?.Select(item => new UpdateSaleItemRequest
            {
                ProductId = item.ProductId,
                ProductUnitPrice = item.ProductUnitPrice,
                Status = item.Status
            }).ToList()
        };

        var updateResponse = await _client.PutAsJsonAsync($"/api/sales/{saleId}", updateRequest);
        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task DeleteSale_ShouldReturn200AndSuccessMessage()
    {
        await AuthenticateAsync();

        // First, create a sale
        var createRequest = SaleControllerData.GenerateSaleWithItems(2);

        var createResponse = await _client.PostAsJsonAsync("/api/sales", createRequest);
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var createContent = await createResponse.Content.ReadAsStringAsync();
        var createdSale = JsonDocument.Parse(createContent).RootElement.GetProperty("data").GetProperty("data");
        var saleId = createdSale.GetProperty("id").GetGuid();

        // Delete
        var deleteResponse = await _client.DeleteAsync($"/api/sales/{saleId}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task ListSales_ShouldReturn200AndPaginatedSales()
    {
        await AuthenticateAsync();

        // Ensure at least one sale exists
        var createRequest = SaleControllerData.GenerateSaleWithItems(2);

        var createResponse = await _client.PostAsJsonAsync("/api/sales", createRequest);
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var response = await _client.GetAsync("/api/sales?page=1&size=10");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("items");
        content.Should().Contain("currentPage");
        content.Should().Contain("totalPages");
    }
}