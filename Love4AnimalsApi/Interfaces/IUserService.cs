using Love4AnimalsApi.Dtos;
namespace Love4AnimalsApi.Interfaces;
public interface IUserService
{
    List<GetUserDto> GetUsers();
    GetUserDto? GetUser(long id);
    GetUserDto CreateUser(CreateUserDto createUserDto);
    GetUserDto? UpdateUser(long id, UpdateUserDto updateUserDto);
    bool DeleteUser(long id);
}
