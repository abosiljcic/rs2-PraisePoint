using AutoMapper;
using EventBus.Messages.Events;
using MediatR;
using Reward.API.Repositories.Interfaces;

namespace Reward.API.Features.Commands.UpdatePoints
{
    public class UpdatePointsHandler : IRequestHandler<UpdatePointsCommand, Boolean>
    {
        private readonly IPointsRepository _pointsRepository;
        private readonly IMapper _mapper;

        public UpdatePointsHandler(IPointsRepository pointsRepository, IMapper mapper)
        {
            _pointsRepository = pointsRepository ?? throw new ArgumentNullException(nameof(pointsRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> Handle(UpdatePointsCommand request, CancellationToken cancellationToken)
        {
            var awardPointsEvent = _mapper.Map<AwardPointsEvent>(request);
            var sender = await _pointsRepository.GetPointsForUserById(awardPointsEvent.SenderId);
            var receiver = await _pointsRepository.GetPointsForUserById(awardPointsEvent.ReceiverId);

            if (sender == null || receiver == null)
            {
                return false;
            }

            //fix
            if (sender.budget < request.Points)
            {
                return false;
            }

            sender.budget -= awardPointsEvent.Points;
            receiver.received_points += awardPointsEvent.Points;

            var updateSenderResult = await _pointsRepository.UpdateUserAsync(sender);
            var updateReceiverResult = await _pointsRepository.UpdateUserAsync(receiver);

            return updateSenderResult && updateReceiverResult;
        }
    }
}
