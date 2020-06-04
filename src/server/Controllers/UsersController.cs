using DaHo.M151.DataFormatValidator.Abstractions;
using DaHo.M151.DataFormatValidator.Models.ApiModels;
using DaHo.M151.DataFormatValidator.Models.ServiceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DaHo.M151.DataFormatValidator.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest model)
        {
            var user = await _userService.Authenticate(model.Username, model.Password);

            if (user == null)
            {
                return BadRequest("Username or password is incorrect");
            }

            return Ok(user);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            // only allow admins to access other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Admin))
            {
                return Forbid();
            }

            var user = await _userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
