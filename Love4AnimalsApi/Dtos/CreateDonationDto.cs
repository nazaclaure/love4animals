namespace Love4AnimalsApi.Dtos
{
    public class CreateDonationDto
    {
        public double Amount { get; set; }
        public long UserId { get; set; }
        public long CampaignId { get; set; }
    }
}
