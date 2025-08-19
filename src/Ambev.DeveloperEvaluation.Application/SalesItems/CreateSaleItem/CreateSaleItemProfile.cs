using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.SalesItems.CreateSaleItem
{
    public class CreateSaleItemProfile : Profile
    {
        public CreateSaleItemProfile()
        {
            CreateMap<CreateSaleItemCommand, SaleItem>();
            CreateMap<SaleItem, CreateSaleItemResult>();
        }
    }
}
