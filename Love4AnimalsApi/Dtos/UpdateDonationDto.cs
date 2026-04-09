using System.ComponentModel.DataAnnotations;
namespace Love4AnimalsApi.Dtos;

public class UpdateDonationDto
{
    [Required][Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }
    [MaxLength(500)]
    public string? Message { get; set; }
    [Required][MaxLength(50)]
    public string Status { get; set; } = "";
}
