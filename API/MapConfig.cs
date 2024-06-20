using AutoMapper;
using Entity.DTO.Patient;
using Entity.Models;

namespace API;

public class MapConfig : Profile
{
    public MapConfig()
    {
        CreateMap<User, PatientProfile>().ReverseMap();

        CreateMap<PatientDetails, RequestClient>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.RegionId, opt => opt.MapFrom(src => src.regionId))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Mobile))
            .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Symptoms))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode.ToString()))
            .ForMember(dest => dest.StrMonth, opt => opt.MapFrom(src => src.Bdate.Month.ToString()))
            .ForMember(dest => dest.IntDate, opt => opt.MapFrom(src => src.Bdate.Day))
            .ForMember(dest => dest.IntYear, opt => opt.MapFrom(src => src.Bdate.Year));

        CreateMap<OtherRequest, RequestClient>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.RegionId, opt => opt.MapFrom(src => src.regionId))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Mobile))
            .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Symptoms))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode.ToString()));

        CreateMap<PatientDetails, Request>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Mobile))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.RequestTypeId, opt => opt.MapFrom(src => 2))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1))
            .ForMember(dest => dest.IsUrgentEmailSent, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));

        CreateMap<PatientDetails, User>()
        .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
        .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
        .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
        .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.Mobile))
        .ForMember(dest => dest.RegionId, opt => opt.MapFrom(src => src.regionId))
        .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
        .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
        .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode.ToString()))
        .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.FirstName))
        .ForMember(dest => dest.StrMonth, opt => opt.MapFrom(src => src.Bdate.Month.ToString()))
        .ForMember(dest => dest.IntDate, opt => opt.MapFrom(src => src.Bdate.Day))
        .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
        .ForMember(dest => dest.IntYear, opt => opt.MapFrom(src => src.Bdate.Year))
        .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1))
        .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));

        CreateMap<OtherRequest, User>()
        .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
        .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
        .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
        .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.Mobile))
        .ForMember(dest => dest.RegionId, opt => opt.MapFrom(src => src.regionId))
        .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
        .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
        .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode.ToString()))
        .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.FirstName))
        .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
        .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1))
        .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));

        CreateMap<User,PatientProfile>().ReverseMap();

        CreateMap<PatientDetails, AspNetUser>()
        .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.FirstName+ " " + src.LastName))
        .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
        .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now));

        
        
    }
}
