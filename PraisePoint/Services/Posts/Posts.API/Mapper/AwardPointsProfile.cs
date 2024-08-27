using AutoMapper;
using EventBus.Messages.Events;
using Posts.Domain.Entities;

namespace Posts.API.Mapper
{
    public class AwardPointsProfile : Profile
    {
        public AwardPointsProfile() 
        {
            CreateMap<AwardPoints, AwardPointsEvent>().ReverseMap();
        }
    }
}
