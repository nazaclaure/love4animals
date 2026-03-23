using Love4AnimalsApi.Dtos;
namespace Love4AnimalsApi.Interfaces;
public interface ICampaignService
{
    public List<GetCampaignDto> GetCampaigns();
    public GetCampaignDto GetCampaign(long id);
    public GetCampaignDto CreateCampaign(CreateCampaignDto createCampaignDto);
    public GetCampaignDto UpdateCampaign(long id, UpdateCampaignDto updateCampaignDto);
    public void DeleteCampaign(long id);
}
