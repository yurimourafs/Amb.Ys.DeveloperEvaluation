using Ambev.DeveloperEvaluation.Application.SalesItems.UpdateSaleItem;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.UpdateSaleItem;

/// <summary>
/// AutoMapper profile for UpdateSaleItem mappings.
/// </summary>
public class UpdateSaleItemProfile : Profile
{
    public UpdateSaleItemProfile()
    {
        CreateMap<UpdateSaleItemRequest, UpdateSaleItemCommand>();
        CreateMap<UpdateSaleItemResult, UpdateSaleItemResponse>();
    }
}