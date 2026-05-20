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
    public GetUserDto? GetUser(long id)
    {
        User? user = userRepository.GetUser(id);
        if (user == null) return null;
        return new GetUserDto(user.Id, user.Name, user.Email, user.ProfilePicture);
    }
    public GetUserDto CreateUser(CreateUserDto createUserDto)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password, workFactor: 12);
        User user = new User
        {
            Name = createUserDto.Name,
            Email = createUserDto.Email,
            PasswordHash = passwordHash,
            ProfilePicture = createUserDto.ProfilePicture
        };
        User createdUser = userRepository.CreateUser(user);
        return new GetUserDto(createdUser.Id, createdUser.Name, createdUser.Email, createdUser.ProfilePicture);
    }
    public GetUserDto? UpdateUser(long id, UpdateUserDto updateUserDto)
    {
        User? existing = userRepository.GetUser(id);
        if (existing == null) return null;
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(updateUserDto.Password, workFactor: 12);
        existing.Name = updateUserDto.Name;
        existing.Email = updateUserDto.Email;
        existing.PasswordHash = passwordHash;
        existing.ProfilePicture = updateUserDto.ProfilePicture;
        User? updatedUser = userRepository.UpdateUser(id, existing);
        if (updatedUser == null) return null;
        return new GetUserDto(updatedUser.Id, updatedUser.Name, updatedUser.Email, updatedUser.ProfilePicture);
    }
    public bool DeleteUser(long id)
    {
        return userRepository.DeleteUser(id);
    }
    public GetUserDto? Login(LoginDto loginDto)
    {
        User? user = userRepository.GetUserByEmail(loginDto.Email);
        if (user == null) return null;
        bool valid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
        if (!valid) return null;
        return new GetUserDto(user.Id, user.Name, user.Email, user.ProfilePicture);
    }
}
