using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Posts.Application.Features.Posts.Commands.CreatePost;
using Posts.Application.Features.Posts.Queries.GetPostById;
using Posts.Application.Features.Posts.Queries.GetPostsByCompanyId;
using Posts.Application.Features.Posts.Queries.GetPostsByHashtagId;
using Posts.Application.Features.Posts.Queries.GetPostsByReceiverUsername;
using Posts.Application.Features.Posts.Queries.GetPostsByUsername;
using Posts.Application.Features.Posts.Queries.ViewModels;
using Posts.Domain.Entities;
using Posts.Application.Contracts.Infrastructure;
using Posts.Application.Features.Posts.Commands.AddComment;
using Posts.Application.Features.Posts.Commands.AddLikeCommand;

namespace Posts.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<PostController> _logger;
        private readonly IUserService _userService;

        public PostController(IMediator mediator, IMapper mapper, IPublishEndpoint publishEndpoint,
            ILogger<PostController> logger, IUserService userService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet("/company/{companyId}")]
        [ProducesResponseType(typeof(IEnumerable<PostViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PostViewModel>>> GetPostsByCompanyId(Guid companyId)
        {
            var query = new GetPostsByCompanyIdQuery(companyId);
            var posts = await _mediator.Send(query);
            var sortedPosts = posts.OrderByDescending(post => post.CreatedDate).ToList();

            return Ok(sortedPosts);
        }

        [HttpGet("/sender-username/{senderUsername}")]
        [ProducesResponseType(typeof(IEnumerable<PostViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PostViewModel>>> GetPostsBySenderUsername(string senderUsername)
        {
            var query = new GetPostsBySenderUsernameQuery(senderUsername);
            var posts = await _mediator.Send(query);
            var sortedPosts = posts.OrderByDescending(post => post.CreatedDate).ToList();

            return Ok(sortedPosts);
        }

        [HttpGet("/receiver-username/{receiverUsername}")]
        [ProducesResponseType(typeof(IEnumerable<PostViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PostViewModel>>> GetPostsByReceiverUsername(string receiverUsername)
        {
            var query = new GetPostsByReceiverUsernameQuery(receiverUsername);
            var posts = await _mediator.Send(query);
            var sortedPosts = posts.OrderByDescending(post => post.CreatedDate).ToList();

            return Ok(sortedPosts);
        }

        [HttpGet("/hashtagId/{hashtagId}")]
        [ProducesResponseType(typeof(IEnumerable<PostViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PostViewModel>>> GetPostsByHashtagId(Guid hashtagId)
        {
            var query = new GetPostsByHashtagIdQuery(hashtagId);
            var posts = await _mediator.Send(query);
            var sortedPosts = posts.OrderByDescending(post => post.CreatedDate).ToList();

            return Ok(sortedPosts);
        }

        [HttpGet("/id/{id}")]
        [ProducesResponseType(typeof(PostViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<PostViewModel>> GetPostById(Guid id)
        {
            var query = new GetPostByIdQuery(id);
            var post = await _mediator.Send(query);
            return Ok(post);
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand command)
        {
            _logger.LogInformation("Sending command: CreatePostCommand : ({@Command})", command);
            var postId = await _mediator.Send(command);
            
            if(postId == Guid.Empty) return BadRequest("User tried to send more points than they have.");

            var awardPoints = new AwardPoints(command.SenderUsername, command.ReceiverUsername, command.Points);
            var eventMessage = _mapper.Map<AwardPointsEvent>(awardPoints);
            
            _logger.LogInformation("Publishing message: {}", eventMessage);
            await _publishEndpoint.Publish(eventMessage);

            return Accepted();
        }

        [HttpPost("/comments")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddComment([FromBody] AddCommentCommand command)
        {
            _logger.LogInformation($"Sending command: AddCommentCommand : ({command})");
            var created = await _mediator.Send(command);
            if(!created)
            {
                return BadRequest("Couldn't add a new comment to a post.");
            }

            return Ok();
        }
        
        [HttpPost("/likes")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ToggleLike([FromBody] ToggleLikeCommand command)
        {
            _logger.LogInformation($"Sending command: ToggleLikeCommand : ({command})");
            var success = await _mediator.Send(command);
            if(!success)
            {
                return BadRequest("Couldn't toggle like to a post.");
            }

            return Ok();
        }
    }
}
