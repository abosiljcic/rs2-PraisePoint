using MediatR;

namespace Reward.API.Features.Commands.UpdatePoints
{
    public class UpdatePointsCommand : IRequest<bool>
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public int Points { get; set; }
    }
}
