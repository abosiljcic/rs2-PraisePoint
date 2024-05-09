using Microsoft.AspNetCore.Mvc;
using Reward.API.Entities;
using Reward.API.Repositories.Interfaces;

namespace Reward.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RewardController : ControllerBase
    {
        IPointsRepository _repository;

        public RewardController(IPointsRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Points>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Points>>> GetAllPoints()
        {
            var allPoints = await _repository.GetAllPoints();
            return Ok(allPoints);   
        }
    }
}
