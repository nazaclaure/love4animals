namespace Project1202.Dto;

public record GetProductDto (
    string Code,
    ProductDto Product,
    int Status,
    string Status_verbose
);