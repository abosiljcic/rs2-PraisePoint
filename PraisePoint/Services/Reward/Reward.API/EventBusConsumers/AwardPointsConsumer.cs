using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Reward.API.Features.Commands.UpdatePoints;

namespace Reward.API.EventBusConsumers
{
    public class AwardPointsConsumer : IConsumer<AwardPointsEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<AwardPointsConsumer> _logger;

        public AwardPointsConsumer(IMediator mediator, IMapper mapper, ILogger<AwardPointsConsumer> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<AwardPointsEvent> context)
        {
            var command = _mapper.Map <UpdatePointsCommand>(context.Message);
            var id = await _mediator.Send(command);

            _logger.LogInformation($"{typeof(AwardPointsEvent).Name} consumed successfully.{id}");

        }
    }
}
