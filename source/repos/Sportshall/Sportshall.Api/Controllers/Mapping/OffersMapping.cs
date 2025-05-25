using AutoMapper;

namespace Sportshall.Api.Controllers.Mapping
{
    public class OffersMapping : Profile
    {
        public OffersMapping()
        {
            CreateMap<Sportshall.Core.Entites.Offers, Sportshall.Core.DTO.OffersDTO>()
                .ForMember(dest => dest.ActivitiesName, opt => opt.MapFrom(src => src.Activities.Name))
                .ReverseMap();


            CreateMap<Sportshall.Core.DTO.AddOffersDTO, Sportshall.Core.Entites.Offers>().ReverseMap();

            //CreateMap<Sportshall.Core.DTO.AddOffersDTO, Sportshall.Core.Entites.Offers>()
            //    .ForMember(dest => dest.Photos, opt => opt.Ignore())
            //    .ReverseMap();

            //CreateMap<Sportshall.Core.DTO.UpdateOffersDTO, Sportshall.Core.Entites.Offers>()
            //    .ForMember(dest => dest.Photos, opt => opt.Ignore())
            //    .ReverseMap();
        }
    }
    
}
