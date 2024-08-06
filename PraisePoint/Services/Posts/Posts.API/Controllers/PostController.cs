using MediatR;
using Microsoft.AspNetCore.Mvc;
using Posts.Application.Features.Posts.Queries.GetPostById;
using Posts.Application.Features.Posts.Queries.GetPostsByCompanyId;
using Posts.Application.Features.Posts.Queries.GetPostsByHashtagId;
using Posts.Application.Features.Posts.Queries.GetPostsByUsername;
using Posts.Application.Features.Posts.Queries.ViewModels;

namespace Posts.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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
        [ProducesResponseType(typeof(PostViewModel
            ), StatusCodes.Status200OK)]
        public async Task<ActionResult<PostViewModel>> GetPostById(Guid id)
        {
            var query = new GetPostByIdQuery(id);
            var post = await _mediator.Send(query);
            return Ok(post);
        }

    }
}
