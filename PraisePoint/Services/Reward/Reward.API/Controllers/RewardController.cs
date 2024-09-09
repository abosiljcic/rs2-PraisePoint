using Microsoft.AspNetCore.Mvc;
using Reward.API.Entities;
using Reward.API.Repositories.Interfaces;

namespace Reward.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
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

        [HttpGet("/users/{username}")]
        [ProducesResponseType(typeof(Points), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Points), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Points>> GetPointsForUserByUsername(string username)
        {
            var points = await _repository.GetPointsForUserByUsername(username);
            if (points == null)
            {
                return NotFound(null);
            }
            return Ok(points);
        }

        [HttpGet("/companies/{company_id}")]
        [ProducesResponseType(typeof(IEnumerable<Points>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Points>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Points>> GetPointsForCompanyById(string company_id)
        {
            var points = await _repository.GetPointsForCompanyById(company_id);
            if (points == null)
            {
                return NotFound(null);
            }
            return Ok(points);
        }
    }
}
