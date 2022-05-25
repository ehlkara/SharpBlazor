using AutoMapper;
using Sharp_DataAccess;
using Sharp_DataAccess.ViewModel;
using Sharp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp_Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductPrice, ProductPriceDto>().ReverseMap();
            CreateMap<OrderHeader, OrderHeaderDto>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
        }
    }
}
