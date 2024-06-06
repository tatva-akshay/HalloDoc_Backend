using AutoMapper;
using Entity.DTO.Patient;
using Entity.Models;

namespace API;

public class MapConfig : Profile
{
    public MapConfig()
    {
        CreateMap<User, PatientProfile>().ReverseMap();
    }
}
