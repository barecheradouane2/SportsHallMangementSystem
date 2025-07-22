using AutoMapper;

namespace Sportshall.Api.Controllers.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<Sportshall.Core.DTO.UserDTO, Sportshall.Core.Entites.AppUser>()
                .ReverseMap();

            CreateMap<Sportshall.Core.DTO.UserDTODetails, Sportshall.Core.Entites.AppUser>()
                .ReverseMap();

        }
    }
    
}
