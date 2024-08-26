using AutoMapper;
using User.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User.API.Services;
using User.API.Entities;
using EventBus.Messages.Events;
using MassTransit;
using MassTransit.Transports;
using Microsoft.Extensions.Logging;

namespace User.API.Controllers
{

    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<Entities.User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IPublishEndpoint _publishEndpoint;

        public UserController(UserManager<Entities.User> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IUserService userService, IPublishEndpoint publishEndpoint)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("users/{companyId}")]
        [ProducesResponseType(typeof(IEnumerable<UserDetailsDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserDetailsDto>>> GetAllUsersByCompany(Guid companyId)
        {
            var users = await _userManager.Users.Where(u => u.CompanyId == companyId).ToListAsync();

            if (users == null || users.Count == 0)
            {
                return NotFound();
            }

            return Ok(users);
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("{username}")]
        [ProducesResponseType(typeof(UserDetailsDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDetailsDto>> GetUser(string username)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(user => user.UserName == username);
            return Ok(_mapper.Map<UserDetailsDto>(user));
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            // Find the user by the provided userId
            var user = await _userService.GetUserById(userId);

            if (user == null)
            {
                return NotFound($"User with ID {userId} was not found.");
            }

            // Optionally map the user to a DTO (Data Transfer Object) if needed
            return Ok(user);
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("myInfo")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _userService.GetCurrentUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                user.Id,
                user.UserName,
                user.Email,
                user.CompanyId,
                user.FirstName,
                user.LastName,
                user.PhoneNumber
            });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPointsNumber(Guid companyId)
        {
            var pointsNumber = await _userService.GetCompanyPointsNumber(companyId);

            if (pointsNumber == null)
            {
                return NotFound($"{pointsNumber} was not found.");
            }

            var eventMessage = _mapper.Map<NewPointsEvent>(pointsNumber);
            await _publishEndpoint.Publish(eventMessage);

            return Accepted();
        }
    }
}
