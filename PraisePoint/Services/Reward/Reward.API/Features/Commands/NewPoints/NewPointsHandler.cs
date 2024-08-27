using AutoMapper;
using EventBus.Messages.Events;
using MediatR;
using Reward.API.Entities;
using Reward.API.Features.DTOs;
using Reward.API.Repositories.Interfaces;

namespace Reward.API.Features.Commands.NewPoints
{
    public class NewPointsHandler : IRequestHandler<NewPointsCommand, bool>
    {
        private readonly IPointsRepository _pointsRepository;
        private readonly IMapper _mapper;

        public NewPointsHandler(IPointsRepository pointsRepository, IMapper mapper)
        {
            _pointsRepository = pointsRepository ?? throw new ArgumentNullException(nameof(pointsRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> Handle(NewPointsCommand request, CancellationToken cancellationToken)
        {
            /*var dto = new NewPointsDto
            {
                UserId = request.UserId,
                CompanyId = request.CompanyId,
                CompanyBudget = request.CompanyBudget
            };*/

            //var newPoints = _mapper.Map<Points>(dto);
            var newPointsEvent = _mapper.Map<NewPointsEvent>(request);
            var newPoints = _mapper.Map<Points>(newPointsEvent);
            
            var result = await _pointsRepository.AddAsync(newPoints);
            
            return result;
        }
    }
}
