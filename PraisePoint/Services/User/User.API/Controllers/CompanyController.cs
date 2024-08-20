using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User.API.Services;

namespace User.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IUserService _userService;

        public CompanyController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _userService.GetCompanies();

            if (companies == null)
            {
                return NotFound($"{companies} was not found.");
            }

            return Ok(companies);
        }
    }
}
