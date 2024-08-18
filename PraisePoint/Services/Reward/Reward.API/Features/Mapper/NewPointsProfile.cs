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
        public NewPointsProfile()
        {
            // NewPointsEvent -> Points
            CreateMap<NewPointsEvent, Points>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Budget, opt => opt.MapFrom(src => src.CompanyBudget))
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId.ToString()))
            .ForMember(dest => dest.ReceivedPoints, opt => opt.MapFrom(src => 0));

            // NewPointsEvent -> NewPointsCommand
            CreateMap<NewPointsEvent, NewPointsCommand>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId.ToString()))
            .ForMember(dest => dest.CompanyBudget, opt => opt.MapFrom(src => src.CompanyBudget));

        }
    }
}
