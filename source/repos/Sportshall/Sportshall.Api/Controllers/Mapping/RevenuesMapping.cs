using AutoMapper;

namespace Sportshall.Api.Controllers.Mapping
{
    public class RevenuesMapping : Profile
    {
        public RevenuesMapping()
        {
            CreateMap<Sportshall.Core.Entites.Revenues, Sportshall.Core.DTO.RevenuesDTO>().ReverseMap();

            CreateMap<Sportshall.Core.DTO.AddRevenuesDTO, Sportshall.Core.Entites.Revenues>()

                .ReverseMap();

            CreateMap<Sportshall.Core.DTO.UpdateRevenuesDTO, Sportshall.Core.Entites.Revenues>()
                .ReverseMap();
        }
    }
    
}
