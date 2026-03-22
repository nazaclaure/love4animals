namespace Project1202.Dto;

public record OrderProductDto(
    string Code,
    string Name,
    int Quantity,
    double? Total
);