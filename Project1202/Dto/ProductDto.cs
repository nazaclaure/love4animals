using System.Security.Permissions;

namespace Project1202.Dto;

public record ProductDto (
    string Product_name,
    List<string> Categories_tags,
    string Image_url
);