using AutoMapper;
using EventBus.Messages.Events;
using Reward.API.Entities;
using Reward.API.Features.Commands.NewPoints;
using Reward.API.Features.Commands.UpdatePoints;
using Reward.API.Features.DTOs;

namespace Reward.API.Features.Mapper
{
    public class NewPointsProfile : Profile
    {
        public NewPointsProfile() {
            /*CreateMap<NewPointsDto, Points>()
                .ForMember(dest => dest.user_id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.company_id, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.budget, opt => opt.MapFrom(src => src.CompanyBudget))
                .ForMember(dest => dest.received_points, opt => opt.MapFrom(src => 0))
                .ReverseMap(); // Postavlja received_points na 0*/
            CreateMap<NewPointsEvent, Points>()
                .ForMember(dest => dest.user_id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.company_id, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.budget, opt => opt.MapFrom(src => src.CompanyBudget))
                .ForMember(dest => dest.received_points, opt => opt.MapFrom(src => 0)) // Postavlja received_points na 0
                .ReverseMap();

            CreateMap<NewPointsCommand, EventBus.Messages.Events.NewPointsEvent>().ReverseMap();
        }
    }
}
