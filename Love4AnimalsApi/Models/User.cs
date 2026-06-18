namespace Love4AnimalsApi.Models;
public class User
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string ProfilePicture { get; set; } = string.Empty;
    public string Role { get; set; } = "Donador";
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
}
//campo role, refresh para renovar el token en bd, expiry cada cuanto se va a expirar, 7 dias