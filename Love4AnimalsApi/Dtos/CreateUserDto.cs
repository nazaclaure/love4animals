using System.ComponentModel.DataAnnotations;
namespace Love4AnimalsApi.Dtos;

public class CreateUserDto
{
    [Required][MaxLength(100)]
    public string Name { get; set; } = "";
    [Required][EmailAddress][MaxLength(200)]
    public string Email { get; set; } = "";
    [Required][MinLength(6)][MaxLength(100)]
    public string Password { get; set; } = "";
    [Required]
    public string ProfilePicture { get; set; } = "";
    [Required][RegularExpression("^(Misionero|Donador)$", ErrorMessage = "El rol debe ser Misionero o Donador.")]
    public string Role { get; set; } = "Donador";
}
