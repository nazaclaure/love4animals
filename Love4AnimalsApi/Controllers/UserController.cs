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
        public List<GetUserDto> GetUsers()
        {
            return this.userService.GetUsers();
        }
        [HttpGet("{id}")]
        public GetUserDto GetUser(long id)
        {
            return this.userService.GetUser(id);
        }
        [HttpPost("")]
        public GetUserDto CreateUser([FromBody] CreateUserDto createUserDto)
        {
            return this.userService.CreateUser(createUserDto);
        }
        [HttpPut("{id}")]
        public GetUserDto UpdateUser(long id, [FromBody] UpdateUserDto updateUserDto)
        {
            return this.userService.UpdateUser(id, updateUserDto);
        }
        [HttpDelete("{id}")]
        public void DeleteUser(long id)
        {
            this.userService.DeleteUser(id);
        }
    }
}
