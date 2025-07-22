using AutoMapper;
using Sportshall.Core.DTO;
using Sportshall.Core.Entites;

namespace Sportshall.Api.Controllers.Mapping
{
    public class AttendancesMapping : Profile
    {
       

        public AttendancesMapping() { 

            CreateMap<Sportshall.Core.Entites.Attendances, Sportshall.Core.DTO.AttendancesDTO>()
                .ForMember(dest => dest.MemberFullName, opt => opt.MapFrom(src => src.Member.FullName))
                .ForMember(dest => dest.ActivityName, opt => opt.MapFrom(src => src.Activities.Name))
                 .ReverseMap();


            CreateMap<Sportshall.Core.Entites.Attendances, Sportshall.Core.DTO.UpdateAttendancesDTO>().ReverseMap();

            // 🔧 Add this to fix the error
            CreateMap<Sportshall.Core.DTO.AddAttendancesDTO, Sportshall.Core.Entites.Attendances>().ReverseMap();











        }

    }
}
