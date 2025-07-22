using AutoMapper;

namespace Sportshall.Api.Controllers.Mapping
{
    public class MembersMapping : Profile
    {
        public MembersMapping()
        {
            CreateMap<Sportshall.Core.Entites.Members, Sportshall.Core.DTO.MembersDTO>()
                .ReverseMap();

            CreateMap<Sportshall.Core.DTO.AddMembersDTO, Sportshall.Core.Entites.Members>()
                .ReverseMap();
            CreateMap<Sportshall.Core.DTO.UpdateMembersDTO, Sportshall.Core.Entites.Members>()
                .ReverseMap();
        }
    }


   
}
