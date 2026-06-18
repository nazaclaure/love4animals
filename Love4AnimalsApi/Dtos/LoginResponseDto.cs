namespace Love4AnimalsApi.Dtos;
public class LoginResponseDto
{
    public string Token { get; set; } = "";
    public string RefreshToken { get; set; } = "";
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string Role { get; set; } = "";
}

//lo que devuelve el login