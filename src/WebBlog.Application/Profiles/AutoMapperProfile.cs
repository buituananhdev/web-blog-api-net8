using AutoMapper;
using WebBlog.Domain.Enums;
using WebBlog.Application.Dtos;
using WebBlog.Domain.Entities;

namespace WebBlog.Application.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User profile
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, RegistrationDto>().ReverseMap();
            CreateMap<UpdateUserDto, User>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<AdminUpdateUserDto, User>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Registration profile
            CreateMap<RegistrationDto, UserDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Avatar, opt => opt.Ignore()) // Ignore Avatar for registration
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => Status.Inactive)); // Set default IsActive status
        }
    }
}
