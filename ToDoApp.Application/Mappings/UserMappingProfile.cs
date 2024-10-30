using AutoMapper;
using ToDoApp.Domain.Entities;
using ToDoApp.Infrastructure.Entities;

namespace ToDoApp.Application.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, IdentityUserEntity>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();
        }
    }
}
