using System;

namespace Love4AnimalsApi.Dtos
{
    public class GetDonationDto
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;
        public long UserId { get; set; }
        public long CampaignId { get; set; }
        public string? Message { get; set; }
    }
}