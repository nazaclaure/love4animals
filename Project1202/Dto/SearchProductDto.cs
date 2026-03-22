namespace Project1202.Dto;

public record SearchProductDto(
    List<ProductItemDto> Products
);

public record ProductItemDto(
    string? Code,
    string? Product_name,
    List<string>? Categories_tags,
    string? Image_url
);