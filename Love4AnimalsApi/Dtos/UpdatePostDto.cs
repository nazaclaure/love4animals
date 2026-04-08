using System.ComponentModel.DataAnnotations;
namespace Love4AnimalsApi.Dtos;
public class UpdatePostDto
{
    [Required]
    public string Description { get; set; } = "";
    [Required]
    public string ImageURL { get; set; } = "";
    [Required]
    public long CampaignId { get; set; }
}
