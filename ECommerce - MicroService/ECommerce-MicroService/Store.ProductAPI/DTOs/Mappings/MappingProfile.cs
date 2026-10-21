using AutoMapper;
using Store.ProductAPI.Models;

namespace Store.ProductAPI.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {            
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<ProductDTO, Product>();

            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

        }
    }
}
