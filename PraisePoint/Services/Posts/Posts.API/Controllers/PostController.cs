using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Posts.Application.Features.Posts.Commands.CreatePost;
using Posts.Application.Features.Posts.Queries.GetPostById;
using Posts.Application.Features.Posts.Queries.GetPostsByCompanyId;
using Posts.Application.Features.Posts.Queries.GetPostsByHashtagId;
using Posts.Application.Features.Posts.Queries.GetPostsByUsername;
using Posts.Application.Features.Posts.Queries.ViewModels;
using Posts.Domain.Entities;
using System.Security.Claims;

namespace Posts.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public PostController(IMediator mediator, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        [HttpGet("/company/{companyId}")]
        [ProducesResponseType(typeof(IEnumerable<PostViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PostViewModel>>> GetPostsByCompanyId(Guid companyId)
        {
            var query = new GetPostsByCompanyIdQuery(companyId);
            var posts = await _mediator.Send(query);
            return Ok(posts);
        }

        [HttpGet("/username/{username}")]
        [ProducesResponseType(typeof(IEnumerable<PostViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PostViewModel>>> GetPostsByUsername(string username)
        {
            var query = new GetPostsByUsernameQuery(username);
            var posts = await _mediator.Send(query);
            return Ok(posts);
        }

        [HttpGet("/hashtagId/{hashtagId}")]
        [ProducesResponseType(typeof(IEnumerable<PostViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PostViewModel>>> GetPostsByHashtagId(Guid hashtagId)
        {
            var query = new GetPostsByHashtagIdQuery(hashtagId);
            var posts = await _mediator.Send(query);
            return Ok(posts);
        }

        [HttpGet("/id/{id}")]
        [ProducesResponseType(typeof(PostViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<PostViewModel>> GetPostById(Guid id)
        {
            var query = new GetPostByIdQuery(id);
            var post = await _mediator.Send(query);
            return Ok(post);
        }

        // api/v1/post  kreiranje posta i asinhrono slanje reward servisu 
        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand command)
        {

            var postId = await _mediator.Send(command);
            if(postId == null)
            {
                return BadRequest();
            }

            var pointsAwarded = new PointsAwarded(command.SenderUsername, command.ReceiverUsername, command.Points);
            var eventMessage = _mapper.Map<PointsAwardedEvent>(pointsAwarded);
            await _publishEndpoint.Publish(eventMessage);

            return Accepted();
        }

    }
}
