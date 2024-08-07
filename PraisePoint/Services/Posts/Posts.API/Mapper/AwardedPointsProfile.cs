using AutoMapper;
using EventBus.Messages.Events;
using Posts.Domain.Entities;

namespace Posts.API.Mapper
{
    public class AwardedPointsProfile : Profile
    {
        public AwardedPointsProfile() 
        {
            CreateMap<PointsAwarded, PointsAwardedEvent>().ReverseMap();
        }
    }
}
