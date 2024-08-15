using AutoMapper;
using Reward.API.Features.Commands.UpdatePoints;

namespace Reward.API.Features.Mapper
{
    public class UpdatePointsProfile : Profile
    {
        public UpdatePointsProfile()
        {
            CreateMap<UpdatePointsCommand, EventBus.Messages.Events.AwardPointsEvent>().ReverseMap();
        }
    }
}
