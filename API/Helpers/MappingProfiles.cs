using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name));
            CreateMap<Product, CreateProductDto>().ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name));
            CreateMap<Product, ProductDto>();  
            CreateMap<OrderDetail, OrderDetailToReturnDto>()
                 .ForMember(d => d.Product, o => o.MapFrom(s => s.Product.ProductName))
                 .ForMember(d => d.Order, o => o.MapFrom(s => s.Order.OrderNo));

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.Customer, o => o.MapFrom(s => s.Customer.FirstName));

            CreateMap<Customer, CustomerDto>();
        }
    }
}