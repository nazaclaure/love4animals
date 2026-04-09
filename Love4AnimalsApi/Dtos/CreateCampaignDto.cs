using System.ComponentModel.DataAnnotations;
namespace Love4AnimalsApi.Dtos;

public class CreateCampaignDto
{
    [Required][MaxLength(100)]
    public string Name { get; set; } = "";
    [Required][MaxLength(500)]
    public string Description { get; set; } = "";
    [Required][Range(0, double.MaxValue)]
    public double FundraisingGoal { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
}
