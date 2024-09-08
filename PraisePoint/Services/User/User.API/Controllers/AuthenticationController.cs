using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User.API.Controllers.Base;
using User.API.DTOs;
using User.API.Services;

namespace User.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : RegistrationControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public AuthenticationController(ILogger<AuthenticationController> logger, IMapper mapper, UserManager<Entities.User> userManager, RoleManager<IdentityRole> roleManager, IAuthenticationService authService, IUserService userService, IPublishEndpoint publishEndpoint)
            : base(logger, mapper, userManager, roleManager)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterEmployee([FromBody] NewUserDto newUser)
        {
            var pointsNumber = await _userService.GetCompanyPointsNumber(newUser.CompanyId);

            if (pointsNumber == null)
            {
                return BadRequest("Invalid CompanyId.");
            }

            _logger.LogInformation($"Points number: {newUser.PointsNumber}, {pointsNumber}");

            var userDetails = new NewUserDto
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                CompanyId = newUser.CompanyId,
                Email = newUser.Email,
                UserName = newUser.UserName,
                PhoneNumber = newUser.PhoneNumber,
                ImageUrl = newUser.ImageUrl,
                PointsNumber = pointsNumber,
                Password = newUser.Password
            };
            
            var initPointsDto = new InitPointsDto
            {
                CompanyId = newUser.CompanyId,
                CompanyBudget = pointsNumber,
                UserName = newUser.UserName,
            };

            var eventMessage = _mapper.Map<NewPointsEvent>(initPointsDto);
            _logger.LogInformation($"Event message: {eventMessage.UserName}, {eventMessage.CompanyBudget}");
            await _publishEndpoint.Publish(eventMessage);

            return await RegisterNewUserWithRoles(userDetails, new string[] { "Employee" });
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAdmin([FromBody] NewUserDto newUser)
        {
            return await RegisterNewUserWithRoles(newUser, new string[] { "Admin" });
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(AuthenticationModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] UserCredentialsDto userCredentials)
        {
            var user = await _authService.ValidateUser(userCredentials);
            if (user is null)
            {
                _logger.LogWarning("{Login}: Authentication failed. Wrong username or password.", nameof(Login));
                return Unauthorized();
            }

            return Ok(await _authService.CreateAuthenticationModel(user));
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(AuthenticationModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<AuthenticationModel>> Refresh([FromBody] RefreshTokenModel refreshTokenCredentials)
        {
            var user = await _userManager.FindByNameAsync(refreshTokenCredentials.UserName);
            if (user is null)
            {
                _logger.LogWarning("{Refresh}: Refreshing token failed. Unknown username {UserName}.", nameof(Refresh), refreshTokenCredentials.UserName);
                return Forbid();
            }

            var refreshToken = user.RefreshTokens.FirstOrDefault(r => r.Token == refreshTokenCredentials.RefreshToken);
            if (refreshToken is null)
            {
                _logger.LogWarning("{Refresh}: Refreshing token failed. The refresh token is not found.", nameof(Refresh));
                return Unauthorized();
            }

            if (refreshToken.ExpiryTime < DateTime.Now)
            {
                _logger.LogWarning("{Refresh}: Refreshing token failed. The refresh token is not valid.", nameof(Refresh));
                return Unauthorized();
            }

            return Ok(await _authService.CreateAuthenticationModel(user));
        }

        [Authorize]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenModel refreshTokenCredentials)
        {
            var user = await _userManager.FindByNameAsync(refreshTokenCredentials.UserName);
            if (user is null)
            {
                _logger.LogWarning("{Logout}: Logout failed. Unknown username {UserName}.", nameof(Logout), refreshTokenCredentials.UserName);
                return Forbid();
            }

            await _authService.RemoveRefreshToken(user, refreshTokenCredentials.RefreshToken);

            return Accepted();
        }

    }
}
