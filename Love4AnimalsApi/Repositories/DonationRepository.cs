using System.Collections.Generic;
using System.Linq;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;

namespace Love4AnimalsApi.Repositories
{
    public class DonationRepository : IDonationRepository
    {
        private static List<Donation> _donations = new List<Donation>
        {
            new Donation { Id = 1, Amount = 50.00m, Date = DateTime.UtcNow, Status = "Completed", UserId = 1, CampaignId = 1, Message = "Test donation" }
        };
        private static long _nextId = 2;

        public List<Donation> GetDonationsByCampaign(long campaignId) =>
            _donations.Where(d => d.CampaignId == campaignId).ToList();

        public Donation? GetDonation(long id) =>
            _donations.FirstOrDefault(d => d.Id == id);

        public Donation CreateDonation(Donation donation)
        {
            donation.Id = _nextId++;
            _donations.Add(donation);
            return donation;
        }

        public Donation? UpdateDonation(long id, Donation updatedData)
        {
            var existing = GetDonation(id);
            if (existing != null) {
                existing.Amount = updatedData.Amount;
                existing.Message = updatedData.Message;
                existing.Status = updatedData.Status;
            }
            return existing;
        }

        public bool DeleteDonation(long id)
        {
            var existing = GetDonation(id);
            if (existing == null) return false;
            _donations.Remove(existing);
            return true;
        }
    }
}
