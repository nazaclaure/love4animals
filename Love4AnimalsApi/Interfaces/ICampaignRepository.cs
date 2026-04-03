using Love4AnimalsApi.Models;
namespace Love4AnimalsApi.Interfaces;
public interface ICampaignRepository
{
    List<Campaign> GetCampaigns();
    Campaign? GetCampaign(long id);
    Campaign CreateCampaign(Campaign campaign);
    Campaign? UpdateCampaign(long id, Campaign campaign);
    bool DeleteCampaign(long id);
}
