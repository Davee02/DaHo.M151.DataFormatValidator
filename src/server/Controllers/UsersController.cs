using DaHo.M151.DataFormatValidator.Abstractions;
using DaHo.M151.DataFormatValidator.Models.ApiModels;
using DaHo.M151.DataFormatValidator.Models.ServiceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaHo.M151.DataFormatValidator.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Tries to authenticate using an username and a password
        /// </summary>
        /// <param name="request">The data required to perform the authentication</param>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest request)
        {
            var user = await _userService.Authenticate(request.Username, request.Password);

            if (user == null)
            {
                return BadRequest("Username or password is incorrect");
            }

            return Ok(user);
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();

            return Ok(users);
        }

        /// <summary>
        /// Gets a user by its id
        /// </summary>
        /// <param name="id">The id of the user to find</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
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
