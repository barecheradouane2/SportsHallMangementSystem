using AutoMapper;

namespace Sportshall.Api.Controllers.Mapping
{
    public class ProductsMapping : Profile
    {
        public ProductsMapping()
        {
            CreateMap<Sportshall.Core.Entites.Products, Sportshall.Core.DTO.ProductsDTO>()
                .ForMember(dest => dest.unitName, opt => opt.MapFrom(src => src.unit.ToString()))
                .ReverseMap();

            CreateMap<Sportshall.Core.Entites.ProductPhoto, Sportshall.Core.DTO.ProductsPhotoDTO>().ReverseMap();


            CreateMap<Sportshall.Core.DTO.AddProductsDTO, Sportshall.Core.Entites.Products>()
                .ForMember(dest => dest.Photos, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Sportshall.Core.DTO.UpdateProductsDTO, Sportshall.Core.Entites.Products>()
                .ForMember(dest => dest.Photos, opt => opt.Ignore())
                .ReverseMap();




        }
    }
   
}
