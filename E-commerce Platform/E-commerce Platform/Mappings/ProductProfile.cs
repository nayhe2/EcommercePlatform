using AutoMapper;
using ECommercePlatform.DTOs;
using ECommercePlatform.Models;
namespace ECommercePlatform.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, CreateProductDto>();
        }
    }
}
