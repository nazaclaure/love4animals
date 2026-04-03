using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;
namespace Love4AnimalsApi.Repositories;
public class CampaignRepository : ICampaignRepository
{
    private List<Campaign> Campaigns { get; set; }
    public CampaignRepository()
    {
        this.Campaigns = [];
        Campaign newCampaign = new(1, "Save the Bears", "Help us save the bears", 1000.0, 0.0, DateTime.Now, DateTime.Now.AddMonths(3));
        this.Campaigns.Add(newCampaign);
    }
    public List<Campaign> GetCampaigns()
    {
        return this.Campaigns;
    }
    public Campaign? GetCampaign(long id)
    {
        return this.Campaigns.FirstOrDefault(c => c.Id == id);
    }
    public Campaign CreateCampaign(Campaign campaign)
    {
        campaign.Id = this.Campaigns.Any() ? this.Campaigns.Max(c => c.Id) + 1 : 1;
        this.Campaigns.Add(campaign);
        return campaign;
    }
    public Campaign? UpdateCampaign(long id, Campaign campaign)
    {
        Campaign? existingCampaign = this.Campaigns.FirstOrDefault(c => c.Id == id);
        if (existingCampaign == null) return null;
        existingCampaign.Name = campaign.Name;
        existingCampaign.Description = campaign.Description;
        existingCampaign.FundraisingGoal = campaign.FundraisingGoal;
        existingCampaign.StartDate = campaign.StartDate;
        existingCampaign.EndDate = campaign.EndDate;
        return existingCampaign;
    }
    public bool DeleteCampaign(long id)
    {
        Campaign? existingCampaign = this.Campaigns.FirstOrDefault(c => c.Id == id);
        if (existingCampaign == null) return false;
        this.Campaigns.Remove(existingCampaign);
        return true;
    }
}
