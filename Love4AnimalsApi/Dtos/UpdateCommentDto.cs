using System.ComponentModel.DataAnnotations;
namespace Love4AnimalsApi.Dtos;
public class UpdateCommentDto
{
    [Required]
    public string Content { get; set; } = "";
}
