using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Love4AnimalsApi.Services;

public class UserService : IUserService
{
    private IUserRepository userRepository;
    private IConfiguration configuration;

    public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        this.userRepository = userRepository;
        this.configuration = configuration;
    }

    public List<GetUserDto> GetUsers()
    {
        return userRepository.GetUsers().Select(u => new GetUserDto(u.Id, u.Name, u.Email, u.ProfilePicture)).ToList();
    }

    public GetUserDto? GetUser(long id)
    {
        User? user = userRepository.GetUser(id);
        if (user == null) return null;
        return new GetUserDto(user.Id, user.Name, user.Email, user.ProfilePicture);
    }

    public GetUserDto CreateUser(CreateUserDto createUserDto)
    {
        if (userRepository.GetUserByEmail(createUserDto.Email) != null)
            throw new ArgumentException("El email ya esta registrado");
        var user = new User
        {
            Name = createUserDto.Name,
            Email = createUserDto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password, workFactor: 12),
            ProfilePicture = createUserDto.ProfilePicture,
            Role = createUserDto.Role
        };
        var created = userRepository.CreateUser(user);
        return new GetUserDto(created.Id, created.Name, created.Email, created.ProfilePicture);
    }

    public GetUserDto? UpdateUser(long id, UpdateUserDto updateUserDto)
    {
        User? user = userRepository.GetUser(id);
        if (user == null) return null;
        var existing = userRepository.GetUserByEmail(updateUserDto.Email);
        if (existing != null && existing.Id != id)
            throw new ArgumentException("El email ya esta en uso por otro usuario");
        user.Name = updateUserDto.Name;
        user.Email = updateUserDto.Email;
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updateUserDto.Password, workFactor: 12);
        user.ProfilePicture = updateUserDto.ProfilePicture;
        User? updated = userRepository.UpdateUser(id, user);
        if (updated == null) return null;
        return new GetUserDto(updated.Id, updated.Name, updated.Email, updated.ProfilePicture);
    }

    public bool DeleteUser(long id)
    {
        return userRepository.DeleteUser(id);
    }

    public LoginResponseDto? Login(LoginDto loginDto)
    {
        User? user = userRepository.GetUserByEmail(loginDto.Email);
        if (user == null) return null;
        if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash)) return null;

        var token = GenerateJwtToken(user);
        var refreshToken = GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        userRepository.UpdateUser(user.Id, user);

        return new LoginResponseDto
        {
            Token = token,
            RefreshToken = refreshToken,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        };
    }

    public LoginResponseDto? RefreshToken(string refreshToken)
    {
        var user = userRepository.GetUsers().FirstOrDefault(u => u.RefreshToken == refreshToken);
        if (user == null || user.RefreshTokenExpiry < DateTime.UtcNow) return null;

        var newToken = GenerateJwtToken(user);
        var newRefreshToken = GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        userRepository.UpdateUser(user.Id, user);

        return new LoginResponseDto
        {
            Token = newToken,
            RefreshToken = newRefreshToken,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        };
    }

    private string GenerateJwtToken(User user)
    {
        var jwt = configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };
        var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(20),
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}
