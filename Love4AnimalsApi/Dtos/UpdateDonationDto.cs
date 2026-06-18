using System.ComponentModel.DataAnnotations;
namespace Love4AnimalsApi.Dtos;

public class UpdateDonationDto
{
    [Required][Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0.")]
    public decimal Amount { get; set; }
    public string? Message { get; set; }
    [Required]
    public string Status { get; set; } = string.Empty;
}
