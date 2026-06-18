using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Love4AnimalsApi.Controllers
{
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

        [HttpGet("")]
        [Authorize]
        [EndpointSummary("Get all users.")]
        [ProducesResponseType<List<GetUserDto>>(200)]
        public ActionResult<List<GetUserDto>> GetUsers()
        {
            return Ok(this.userService.GetUsers());
        }

        [HttpGet("{id}")]
        [Authorize]
        [EndpointSummary("Get a user by ID.")]
        [ProducesResponseType<GetUserDto>(200)]
        [ProducesResponseType(404)]
        public ActionResult<GetUserDto> GetUser(long id)
        {
            var user = this.userService.GetUser(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        [EndpointSummary("Register a new user.")]
        [ProducesResponseType<GetUserDto>(201)]
        [ProducesResponseType(400)]
        public ActionResult<GetUserDto> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            try
            {
                var user = this.userService.CreateUser(createUserDto);
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [EndpointSummary("Login with email and password.")]
        [ProducesResponseType<LoginResponseDto>(200)]
        [ProducesResponseType(401)]
        public ActionResult<LoginResponseDto> Login([FromBody] LoginDto loginDto)
        {
            var response = this.userService.Login(loginDto);
            if (response == null) return Unauthorized();
            return Ok(response);
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        [EndpointSummary("Refresh JWT token.")]
        [ProducesResponseType<LoginResponseDto>(200)]
        [ProducesResponseType(401)]
        public ActionResult<LoginResponseDto> Refresh([FromBody] RefreshTokenDto dto)
        {
            var response = this.userService.RefreshToken(dto.RefreshToken);
            if (response == null) return Unauthorized();
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize]
        [EndpointSummary("Update a user.")]
        [ProducesResponseType<GetUserDto>(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public ActionResult<GetUserDto> UpdateUser(long id, [FromBody] UpdateUserDto updateUserDto)
        {
            var tokenUserId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            if (tokenUserId != id)
                return Unauthorized(new { error = "No puedes editar el perfil de otro usuario." });
            try
            {
                var user = this.userService.UpdateUser(id, updateUserDto);
                if (user == null) return NotFound();
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Misionero")]
        [EndpointSummary("Delete a user.")]
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
