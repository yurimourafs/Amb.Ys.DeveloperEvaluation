using Ambev.DeveloperEvaluation.Application.SalesItems.CreateSaleItem;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.SalesItems.UpdateSaleItem
{
    public class UpdateSaleItemProfile : Profile
    {
        public UpdateSaleItemProfile()
        {
            CreateMap<UpdateSaleItemCommand, SaleItem>();
            CreateMap<SaleItem, UpdateSaleItemResult>();
        }
    }
}
