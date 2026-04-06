/*using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;
namespace Love4AnimalsApi.Services;
public class UserService : IUserService
{
    private IUserRepository userRepository;
    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    public GetUserDto GetUser()
    {
        User getUser = userRepository.getUser();
        GetUserDto response = new(getUser.Id, getUser.Name, getUser.Email);
        return response;
    }
}
*/
/*
using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;
namespace Love4AnimalsApi.Services;
public class UserService : IUserService
{
    private IUserRepository userRepository;
    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    public List<GetUserDto> GetUsers()
    {
        List<User> users = userRepository.GetUsers();
        return users.Select(u => new GetUserDto(u.Id, u.Name, u.Email, u.ProfilePicture)).ToList();
    }
    public GetUserDto GetUser(long id)
    {
        User user = userRepository.GetUser(id);
        return new GetUserDto(user.Id, user.Name, user.Email, user.ProfilePicture);
    }
    public GetUserDto CreateUser(CreateUserDto createUserDto)
    {
        User user = new(0, createUserDto.Name, createUserDto.Email, createUserDto.Password, createUserDto.ProfilePicture);
        User createdUser = userRepository.CreateUser(user);
        return new GetUserDto(createdUser.Id, createdUser.Name, createdUser.Email, createdUser.ProfilePicture);
    }
    public GetUserDto UpdateUser(long id, UpdateUserDto updateUserDto)
    {
        User user = new(id, updateUserDto.Name, updateUserDto.Email, updateUserDto.Password, updateUserDto.ProfilePicture);
        User updatedUser = userRepository.UpdateUser(id, user);
        return new GetUserDto(updatedUser.Id, updatedUser.Name, updatedUser.Email, updatedUser.ProfilePicture);
    }
    public void DeleteUser(long id)
    {
        userRepository.DeleteUser(id);
    }
}
public bool DeleteUser(long id)
{
    userRepository.DeleteUser(id);
    return true;
}*/
using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;
namespace Love4AnimalsApi.Services;
public class UserService : IUserService
{
    private IUserRepository userRepository;
    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    public List<GetUserDto> GetUsers()
    {
        List<User> users = userRepository.GetUsers();
        return users.Select(u => new GetUserDto(u.Id, u.Name, u.Email, u.ProfilePicture)).ToList();
    }
    public GetUserDto GetUser(long id)
    {
        User user = userRepository.GetUser(id);
        return new GetUserDto(user.Id, user.Name, user.Email, user.ProfilePicture);
    }
    public GetUserDto CreateUser(CreateUserDto createUserDto)
    {
        User user = new(0, createUserDto.Name, createUserDto.Email, createUserDto.Password, createUserDto.ProfilePicture);
        User createdUser = userRepository.CreateUser(user);
        return new GetUserDto(createdUser.Id, createdUser.Name, createdUser.Email, createdUser.ProfilePicture);
    }
    public GetUserDto UpdateUser(long id, UpdateUserDto updateUserDto)
    {
        User user = new(id, updateUserDto.Name, updateUserDto.Email, updateUserDto.Password, updateUserDto.ProfilePicture);
        User updatedUser = userRepository.UpdateUser(id, user);
        return new GetUserDto(updatedUser.Id, updatedUser.Name, updatedUser.Email, updatedUser.ProfilePicture);
    }
    public bool DeleteUser(long id)
    {
        userRepository.DeleteUser(id);
        return true;
    }
}