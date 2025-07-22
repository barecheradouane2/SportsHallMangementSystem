using AutoMapper;

namespace Sportshall.Api.Controllers.Mapping
{
    public class ActivitiesMapping : Profile
    {
        public ActivitiesMapping()
        {
            CreateMap<Sportshall.Core.Entites.Activities, Sportshall.Core.DTO.ActivitiesDTO>()
                
                .ReverseMap();

            

            CreateMap<Sportshall.Core.Entites.Photo, Sportshall.Core.DTO.PhotoDTO>().ReverseMap();

            CreateMap<Sportshall.Core.DTO.AddActivitiesDTO, Sportshall.Core.Entites.Activities>()
                .ForMember(dest => dest.Photos, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Sportshall.Core.DTO.UpdateActivitiesDTO, Sportshall.Core.Entites.Activities>()
               .ForMember(dest => dest.Photos, opt => opt.Ignore())
               .ReverseMap();



        }
    }
    
}
