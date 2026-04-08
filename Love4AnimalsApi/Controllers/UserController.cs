using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Love4AnimalsApi.Controllers
{
    [Route("v1/users")]
    [ApiController]
    [Tags("User")]
    public class UserController : ControllerBase
    {
        private IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>Retorna todos los usuarios registrados.</summary>
        [HttpGet("")]
        [EndpointSummary("Get All Users")]
        [ProducesResponseType<List<GetUserDto>>(200)]
        public ActionResult<List<GetUserDto>> GetUsers()
        {
            return Ok(this.userService.GetUsers());
        }

        /// <summary>Retorna un usuario por su ID.</summary>
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

        /// <summary>Crea un nuevo usuario.</summary>
        [HttpPost("")]
        [EndpointSummary("Create User")]
        [Consumes("application/json")]
        [ProducesResponseType<GetUserDto>(201)]
        [ProducesResponseType(400)]
        public ActionResult<GetUserDto> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            var user = this.userService.CreateUser(createUserDto);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        /// <summary>Actualiza un usuario existente.</summary>
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

        /// <summary>Elimina un usuario por su ID.</summary>
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
