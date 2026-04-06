/*using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Love4AnimalsApi.Controllers
{
    [Route("v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpGet("")]
        public ActionResult<List<GetUserDto>> GetUsers()
        {
            return Ok(this.userService.GetUsers());
        }
        [HttpGet("{id}")]
        public ActionResult<GetUserDto> GetUser(long id)
        {
            var user = this.userService.GetUser(id);
            if (user == null) return NotFound();
            return Ok(user);
        }
        [HttpPost("")]
        public ActionResult<GetUserDto> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            var user = this.userService.CreateUser(createUserDto);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        [HttpPut("{id}")]
        public ActionResult<GetUserDto> UpdateUser(long id, [FromBody] UpdateUserDto updateUserDto)
        {
            var user = this.userService.UpdateUser(id, updateUserDto);
            if (user == null) return NotFound();
            return Ok(user);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(long id)
        {
            var result = this.userService.DeleteUser(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
*/
using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Love4AnimalsApi.Controllers
{
    [Route("v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("")]
        [EndpointSummary("Get All Users")]
        public ActionResult<List<GetUserDto>> GetUsers()
        {
            return Ok(this.userService.GetUsers());
        }

        [HttpGet("{id}")]
        [EndpointSummary("Get User By Id")]
        public ActionResult<GetUserDto> GetUser(long id)
        {
            var user = this.userService.GetUser(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost("")]
        [EndpointSummary("Create User")]
        public ActionResult<GetUserDto> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            var user = this.userService.CreateUser(createUserDto);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        [EndpointSummary("Update User")]
        public ActionResult<GetUserDto> UpdateUser(long id, [FromBody] UpdateUserDto updateUserDto)
        {
            var user = this.userService.UpdateUser(id, updateUserDto);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        [EndpointSummary("Delete User")]
        public IActionResult DeleteUser(long id)
        {
            var result = this.userService.DeleteUser(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}