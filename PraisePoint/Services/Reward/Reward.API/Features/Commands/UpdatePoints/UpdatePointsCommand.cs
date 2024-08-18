using MediatR;

namespace Reward.API.Features.Commands.UpdatePoints
{
    public class UpdatePointsCommand : IRequest<bool>
    {
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }
        public int Points { get; set; }
    }
}
