using System.ComponentModel.DataAnnotations;
namespace Love4AnimalsApi.Dtos;

public class CreateDonationDto
{
    [Required]
    public long UserId { get; set; }
    [Required]
    public long CampaignId { get; set; }
    [Required][Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }
    [MaxLength(500)]
    public string? Message { get; set; }
}
