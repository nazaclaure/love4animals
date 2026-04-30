using Love4AnimalsApi.Data;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Love4AnimalsApi.Repositories
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly AppDbContext _context;

        public CampaignRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Campaign> GetCampaigns()
        {
            return _context.Campaigns.AsNoTracking().ToList();
        }

        public Campaign? GetCampaign(long id)
        {
            return _context.Campaigns.Find(id);
        }

        public Campaign CreateCampaign(Campaign campaign)
        {
            _context.Campaigns.Add(campaign);
            _context.SaveChanges();
            return campaign;
        }

        public Campaign? UpdateCampaign(long id, Campaign campaign)
        {
            var existing = _context.Campaigns.Find(id);
            if (existing == null) return null;
            existing.Name = campaign.Name;
            existing.Description = campaign.Description;
            existing.FundraisingGoal = campaign.FundraisingGoal;
            existing.TotalRaised = campaign.TotalRaised;
            existing.StartDate = campaign.StartDate;
            existing.EndDate = campaign.EndDate;
            _context.SaveChanges();
            return existing;
        }

        public bool DeleteCampaign(long id)
        {
            var existing = _context.Campaigns.Find(id);
            if (existing == null) return false;
            _context.Campaigns.Remove(existing);
            _context.SaveChanges();
            return true;
        }
    }
}
