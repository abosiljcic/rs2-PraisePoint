using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using User.API.DTOs;

namespace User.API.Controllers.Base
{
    public class RegistrationControllerBase : ControllerBase
    {
        protected readonly ILogger<AuthenticationController> _logger;
        protected readonly IMapper _mapper;
        protected readonly UserManager<Entities.User> _userManager;
        protected readonly RoleManager<IdentityRole> _roleManager;

        public RegistrationControllerBase(ILogger<AuthenticationController> logger, IMapper mapper, UserManager<Entities.User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        protected async Task<IActionResult> RegisterNewUserWithRoles(NewUserDto newUser, IEnumerable<string> roles)
        {   
            var user = _mapper.Map<Entities.User>(newUser);

            var result = await _userManager.CreateAsync(user, newUser.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            _logger.LogInformation("Successfully registered user: {NewUser}.", user.UserName);

            foreach (var role in roles)
            {
                var roleExists = await _roleManager.RoleExistsAsync(role);
                Console.WriteLine("Role exists: " + roleExists);
                if (roleExists)
                {
                    await _userManager.AddToRoleAsync(user, role);
                    _logger.LogInformation("Added role {AddedRole} to user: {Username}.", role, user.UserName);
                }
                else
                {
                    _logger.LogInformation("Role {NonExistingRole} does not exist.", role);
                }
            }

            return StatusCode(StatusCodes.Status201Created);
        }

    }
}
