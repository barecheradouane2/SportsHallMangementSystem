using AutoMapper;
using Sportshall.Core.DTO;
using Sportshall.Core.Entites;

namespace Sportshall.Api.Controllers.Mapping
{
    public class ProductSalesMapping : Profile
    {


        public ProductSalesMapping() {


            CreateMap<Sportshall.Core.Entites.ProductSales, Sportshall.Core.DTO.ProductSalesDTO>()
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Members != null ? src.Members.FullName : ""))
                .ForMember(dest => dest.ProductSalesItems, opt => opt.MapFrom(src => src.ProductSalesItems))
                .ReverseMap();
              


            CreateMap<Sportshall.Core.DTO.AddProductSalesDTO, Sportshall.Core.Entites.ProductSales>()
                .ReverseMap();

            CreateMap<Sportshall.Core.DTO.UpdateProductSalesDTO, Sportshall.Core.Entites.ProductSales>()
                .ReverseMap();

            CreateMap<Sportshall.Core.Entites.ProductSalesItem, Sportshall.Core.DTO.ProductSalesItemDTO>()
              
                .ReverseMap();

            CreateMap<Sportshall.Core.DTO.AddProductSalesItemDTO, Sportshall.Core.Entites.ProductSalesItem>()
                .ReverseMap();

            CreateMap<Sportshall.Core.DTO.UpdateProductSalesItemDTO, Sportshall.Core.Entites.ProductSalesItem>()
                .ReverseMap();

       

   


















        }


    }
}
