/*namespace Love4AnimalsApi.Dtos;
public record GetUserDto (
    int Id,
    string Name,
    string Email
);*/

namespace Love4AnimalsApi.Dtos;
public record GetUserDto (
    long Id,
    string Name,
    string Email,
    string ProfilePicture
);
/* el Password no va en el DTO de respuesta por seguridad, no queremos devolverlo en las consultas*/