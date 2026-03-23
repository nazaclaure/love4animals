namespace Love4AnimalsApi.Dtos;
public record UpdateUserDto (
    string Name,
    string Email,
    string Password,
    string ProfilePicture
);