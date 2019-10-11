using AutoMapper;
using HackathonApi.Models;

namespace HackathonApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ServiceNowAsset, Asset>()
                .ForMember(dest => dest.Location, src => src.Ignore())
                .ForMember(dest => dest.SupportGroup, src => src.Ignore());

            CreateMap<ServiceNowLocation, Location>();

            CreateMap<ServiceNowSupportGroup, SupportGroup>();
        }
    }
}
