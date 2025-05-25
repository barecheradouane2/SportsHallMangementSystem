using AutoMapper;
using Sportshall.Core.DTO;
using Sportshall.Core.Entites;

namespace Sportshall.Api.Controllers.Mapping
{
    public class ExpensesMappinig : Profile
    {
        public ExpensesMappinig()
        {
            CreateMap<Sportshall.Core.Entites.Expenses, Sportshall.Core.DTO.ExpensesDTO>()
               .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.Type.ToString()))
                      .ReverseMap()
               .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<Expensestype>(src.TypeName)));



            CreateMap<AddExpensesDTO, Expenses>()
       .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
       .ReverseMap()
       .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.Type.ToString()));

            CreateMap<UpdateExpensesDTO, Expenses>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ReverseMap()
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.Type.ToString()));



        }

    }
}
