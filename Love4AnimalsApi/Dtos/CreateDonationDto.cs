using System.ComponentModel.DataAnnotations;
namespace Love4AnimalsApi.Dtos;

public class CreateDonationDto
{
    [Required]
    public long CampaignId { get; set; }
    [Required][Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0.")]
    public decimal Amount { get; set; }
    public string Message { get; set; } = "";
    public long UserId { get; set; }
}
