using System.ComponentModel.DataAnnotations;
namespace Love4AnimalsApi.Dtos;
public class CreateCommentDto
{
    [Required]
    public string Content { get; set; } = "";
    [Required]
    public long UserId { get; set; }
}
