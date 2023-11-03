using Api.Dtos;
using AutoMapper;
using Core.Entities;

namespace Api.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Driver, DriverDto>().ReverseMap();
        CreateMap<Team, TeamDto>().ReverseMap();
    }
}
