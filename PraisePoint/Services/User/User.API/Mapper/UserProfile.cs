using AutoMapper;
using User.API.DTOs;

namespace User.API.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
               CreateMap<Entities.User, NewUserDto>().ReverseMap();
               CreateMap<Entities.User, UserDetailsDto>().ReverseMap();
        }
    }
}
