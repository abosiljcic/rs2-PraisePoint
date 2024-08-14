using MediatR;
using Reward.API.Repositories.Interfaces;

namespace Reward.API.Features.Commands.UpdatePoints
{
    public class UpdatePointsHandler : IRequestHandler<UpdatePointsCommand, Boolean>
    {
        private readonly IPointsRepository _pointsRepository;
        public UpdatePointsHandler(IPointsRepository pointsRepository) 
        {
            _pointsRepository = pointsRepository;
        }

        public async Task<bool> Handle(UpdatePointsCommand request, CancellationToken cancellationToken)
        {
            var sender = await _pointsRepository.GetPointsForUserById(request.SenderId);
            var receiver = await _pointsRepository.GetPointsForUserById(request.ReceiverId);

            if (sender == null || receiver == null)
            {
                return false;
            }

            //fix
            if (sender.budget < request.Points)
            {
                return false;
            }

            sender.budget -= request.Points;
            receiver.received_points += request.Points;

            var updateSenderResult = await _pointsRepository.UpdateUserAsync(sender);
            var updateReceiverResult = await _pointsRepository.UpdateUserAsync(receiver);

            return updateSenderResult && updateReceiverResult;
        }
    }
}
