using AutoMapper;
using ShopProduct.Api.Dtos;
using ShopProduct.Api.Entities;

namespace ShopProduct.Api.Profiles
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            //Source -> Target
            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductAddDto, Product>();
        }
    }
}
