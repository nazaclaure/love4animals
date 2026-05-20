namespace Love4AnimalsApi.Dtos
{
    public class UpdateDonationDto
    {
        public decimal Amount { get; set; }
        public string? Message { get; set; }
        public string Status { get; set; } = string.Empty; 
    }
}