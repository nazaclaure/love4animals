using Love4AnimalsApi.Dtos;
namespace Love4AnimalsApi.Interfaces;
public interface ICampaignService
{
    List<GetCampaignDto> GetCampaigns();
    GetCampaignDto? GetCampaign(long id);
    GetCampaignDto CreateCampaign(CreateCampaignDto createCampaignDto);
    GetCampaignDto? UpdateCampaign(long id, UpdateCampaignDto updateCampaignDto);
    bool DeleteCampaign(long id);
}
