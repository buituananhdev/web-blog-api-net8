using AutoMapper;
using WebBlog.Domain.Enums;
using WebBlog.Application.Dtos;
using WebBlog.Domain.Entities;
using WebBlog.Application.Dtos.Post;

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

            // Post profile
            CreateMap<Post, PostDto>()
                .ForMember(dest => dest.UpvoteCount, opt => opt.MapFrom(src => src.Votes.Count(v => v.VoteType == VoteType.Up)))
                .ForMember(dest => dest.DowVoteCount, opt => opt.MapFrom(src => src.Votes.Count(v => v.VoteType == VoteType.Down)))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User != null ? src.User.Id : (Guid?)null));

            CreateMap<PostDto, Post>()
                .ForMember(dest => dest.Votes, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.Tags, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<CreatePostDto, Post>()
                .ForMember(dest => dest.ViewCount, opt => opt.Ignore()) // Ignore ViewCount (default 0)
                .ForMember(dest => dest.UserId, opt => opt.Ignore()) // UserId should be set after mapping, if needed
                .ForMember(dest => dest.Comments, opt => opt.Ignore()) // Ignore complex navigation properties
                .ForMember(dest => dest.Votes, opt => opt.Ignore())
                .ForMember(dest => dest.Tags, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());
        }
    }
}
