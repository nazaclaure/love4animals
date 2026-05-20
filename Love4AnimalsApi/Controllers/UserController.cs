using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Love4AnimalsApi.Controllers
{
    /// <summary>Manage users.</summary>
    [Route("v1/users")]
    [ApiController]
    [Tags("User")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>Get all users.</summary>
        [HttpGet("")]
        [EndpointSummary("Get All Users")]
        [ProducesResponseType<List<GetUserDto>>(200)]
        public ActionResult<List<GetUserDto>> GetUsers()
        {
            return Ok(this.userService.GetUsers());
        }

        /// <summary>Get a user by ID.</summary>
        [HttpGet("{id}")]
        [EndpointSummary("Get User By Id")]
        [ProducesResponseType<GetUserDto>(200)]
        [ProducesResponseType(404)]
        public ActionResult<GetUserDto> GetUser(long id)
        {
            var user = this.userService.GetUser(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        /// <summary>Register a new user.</summary>
        [HttpPost("register")]
        [EndpointSummary("Register User")]
        [Consumes("application/json")]
        [ProducesResponseType<GetUserDto>(201)]
        [ProducesResponseType(400)]
        public ActionResult<GetUserDto> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            var user = this.userService.CreateUser(createUserDto);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        /// <summary>Login with email and password.</summary>
        [HttpPost("login")]
        [EndpointSummary("Login")]
        [Consumes("application/json")]
        [ProducesResponseType<GetUserDto>(200)]
        [ProducesResponseType(401)]
        public ActionResult<GetUserDto> Login([FromBody] LoginDto loginDto)
        {
            var user = this.userService.Login(loginDto);
            if (user == null) return Unauthorized();
            return Ok(user);
        }

        /// <summary>Update an existing user.</summary>
        [HttpPut("{id}")]
        [EndpointSummary("Update User")]
        [Consumes("application/json")]
        [ProducesResponseType<GetUserDto>(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public ActionResult<GetUserDto> UpdateUser(long id, [FromBody] UpdateUserDto updateUserDto)
        {
            var user = this.userService.UpdateUser(id, updateUserDto);
            if (user == null) return NotFound();
            return Ok(user);
        }

        /// <summary>Delete a user by ID.</summary>
        [HttpDelete("{id}")]
        [EndpointSummary("Delete User")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(long id)
        {
            var result = this.userService.DeleteUser(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
