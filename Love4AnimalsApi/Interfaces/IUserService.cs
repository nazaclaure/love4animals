/*using Love4AnimalsApi.Dtos;
namespace Love4AnimalsApi.Interfaces;
public interface IUserService
{
    public GetUserDto GetUser();
}
*/
using Love4AnimalsApi.Dtos;
namespace Love4AnimalsApi.Interfaces;
public interface IUserService
{
    public List<GetUserDto> GetUsers();
    //public GetUserDto GetUser(long id);
    public GetUserDto CreateUser(CreateUserDto createUserDto);
    //public GetUserDto UpdateUser(long id, UpdateUserDto updateUserDto);
    //public void DeleteUser(long id);
    public GetUserDto? GetUser(long id);
public GetUserDto? UpdateUser(long id, UpdateUserDto updateUserDto);
    public bool DeleteUser(long id);
}