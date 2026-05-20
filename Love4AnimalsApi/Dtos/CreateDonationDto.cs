namespace Love4AnimalsApi.Dtos
{
    public class CreateDonationDto
    {
        public decimal Amount { get; set; }
        public long UserId { get; set; }
        public long CampaignId { get; set; }
        public string? Message { get; set; }
    }
}