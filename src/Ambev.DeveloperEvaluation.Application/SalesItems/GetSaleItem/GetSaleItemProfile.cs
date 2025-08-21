using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.SalesItems.GetSaleItem
{
    public class GetSaleItemProfile : Profile
    {
        public GetSaleItemProfile() 
        {
            CreateMap<SaleItem, GetSaleItemResult>();
        }
    }
}
