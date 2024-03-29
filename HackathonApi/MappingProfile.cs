﻿using AutoMapper;
using HackathonApi.Models;

namespace HackathonApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ServiceNowAsset, Asset>()
                .ForMember(dest => dest.Location, src => src.Ignore())
                .ForMember(dest => dest.OwnedBy, src => src.Ignore())
                .ForMember(dest => dest.SupportGroup, src => src.Ignore());

            CreateMap<ServiceNowLocation, Location>();

            CreateMap<ServiceNowDepartment, SubLocation>();

            CreateMap<ServiceNowSupportGroup, SupportGroup>();

            CreateMap<ServiceNowUser, User>();

            CreateMap<AssetPutRequest, ServiceNowAssetPatchRequest>()
                .ForMember(dest => dest.Location, src => src.MapFrom(x => x.LocationId.ToUuid()))
                .ForMember(dest => dest.Department, src => src.MapFrom(x => x.SubLocationId.ToUuid()))
                .ForMember(dest => dest.OwnedBy, src => src.MapFrom(x => x.OwnedById.ToUuid()))
                .ForMember(dest => dest.SupportGroup, src => src.MapFrom(x => x.SupportGroupId.ToUuid()));
        }
    }
}
