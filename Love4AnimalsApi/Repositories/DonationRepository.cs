using System;
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
            new Donation { Id = 1, Amount = 50.00, Date = DateTime.UtcNow, Status = "Completed", UserId = 1, CampaignId = 1 }
        };
        private static long _nextId = 2;

        public List<Donation> GetDonations() => _donations;
        public Donation? GetDonation(long id) => _donations.Find(d => d.Id == id);
        public List<Donation> GetDonationsByCampaign(long id) => _donations.Where(d => d.CampaignId == id).ToList();

        public Donation CreateDonation(Donation donation)
        {
            donation.Id = _nextId++;
            _donations.Add(donation);
            return donation;
        }

        public Donation? UpdateDonation(Donation donation)
        {
            var existing = GetDonation(donation.Id);
            if (existing != null) {
                existing.Amount = donation.Amount;
                existing.Status = donation.Status;
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
