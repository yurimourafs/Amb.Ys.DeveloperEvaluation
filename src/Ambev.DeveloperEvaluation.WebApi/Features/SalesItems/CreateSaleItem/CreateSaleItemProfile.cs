using Ambev.DeveloperEvaluation.Application.SalesItems.CreateSaleItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.CreateSaleItem
{
    public class CreateSaleItemProfile : Profile
    {
        public CreateSaleItemProfile()
        {
            CreateMap<CreateSaleItemRequest, CreateSaleItemCommand>();
            CreateMap<CreateSaleItemResult, CreateSaleItemResponse>();
        }
    }
}
