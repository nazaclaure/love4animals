using Love4AnimalsApi.Models;
namespace Love4AnimalsApi.Interfaces;
public interface ICampaignRepository
{
    public List<Campaign> GetCampaigns();
    public Campaign GetCampaign(long id);
    public Campaign CreateCampaign(Campaign campaign);
    public Campaign UpdateCampaign(long id, Campaign campaign);
    public void DeleteCampaign(long id);
}
