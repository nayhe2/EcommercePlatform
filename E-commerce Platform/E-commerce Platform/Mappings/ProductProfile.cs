using AutoMapper;
using ECommercePlatform.Models.Dto;
using ECommercePlatform.Models.Entities;
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
