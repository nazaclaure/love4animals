using Love4AnimalsApi.Data;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Love4AnimalsApi.Repositories
{
    public class DonationRepository : IDonationRepository
    {
        private readonly AppDbContext _context;

        public DonationRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Donation> GetDonationsByCampaign(long campaignId)
        {
            return _context.Donations.AsNoTracking()
                .Where(d => d.CampaignId == campaignId)
                .ToList();
        }

        public Donation? GetDonation(long id)
        {
            return _context.Donations.Find(id);
        }

        public Donation CreateDonation(Donation donation)
        {
            _context.Donations.Add(donation);
            _context.SaveChanges();
            return donation;
        }

        public Donation? UpdateDonation(long id, Donation updatedData)
        {
            var existing = _context.Donations.Find(id);
            if (existing == null) return null;
            existing.Amount = updatedData.Amount;
            existing.Message = updatedData.Message;
            existing.Status = updatedData.Status;
            _context.SaveChanges();
            return existing;
        }

        public bool DeleteDonation(long id)
        {
            var existing = _context.Donations.Find(id);
            if (existing == null) return false;
            _context.Donations.Remove(existing);
            _context.SaveChanges();
            return true;
        }
    }
}
