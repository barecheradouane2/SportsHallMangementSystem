using AutoMapper;

namespace Sportshall.Api.Controllers.Mapping
{
    public class SubscriptionsMapping :  Profile
    {
        public SubscriptionsMapping()
        {
            CreateMap<Sportshall.Core.Entites.Subscriptions, Sportshall.Core.DTO.SubscriptionsDTO>()
                .ForMember(dest => dest.OfferName, opt => opt.MapFrom(src => src.Offer.Name))
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.FullName))
                .ForMember(dest => dest.ActivitiesName, opt => opt.MapFrom(src => src.Offer.Activities.Name))
                .ReverseMap();

            CreateMap<Sportshall.Core.DTO.AddSubscriptionsDTO, Sportshall.Core.Entites.Subscriptions>().ReverseMap();
        }


    }
}
