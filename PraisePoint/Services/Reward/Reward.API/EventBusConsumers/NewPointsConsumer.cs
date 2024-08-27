using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Reward.API.Features.Commands.NewPoints;

namespace Reward.API.EventBusConsumers
{
    public class NewPointsConsumer : IConsumer<NewPointsEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;  // event -> komanda
        private readonly ILogger<AwardPointsConsumer> _logger;

        public NewPointsConsumer(IMediator mediator, IMapper mapper, ILogger<AwardPointsConsumer> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<NewPointsEvent> context)
        {
            var command = _mapper.Map<NewPointsCommand>(context.Message);
            var id = await _mediator.Send(command);

            _logger.LogInformation($"{typeof(NewPointsEvent).Name} consumed successfully.{id}");
        }
    }
}
