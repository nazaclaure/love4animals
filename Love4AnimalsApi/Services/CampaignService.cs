using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;
namespace Love4AnimalsApi.Services;
public class CampaignService : ICampaignService
{
    private ICampaignRepository campaignRepository;
    public CampaignService(ICampaignRepository campaignRepository)
    {
        this.campaignRepository = campaignRepository;
    }
    public List<GetCampaignDto> GetCampaigns()
    {
        List<Campaign> campaigns = campaignRepository.GetCampaigns();
        return campaigns.Select(c => new GetCampaignDto(c.Id, c.Name, c.Description, c.FundraisingGoal, c.TotalRaised, c.StartDate, c.EndDate)).ToList();
    }
    public GetCampaignDto GetCampaign(long id)
    {
        Campaign campaign = campaignRepository.GetCampaign(id);
        return new GetCampaignDto(campaign.Id, campaign.Name, campaign.Description, campaign.FundraisingGoal, campaign.TotalRaised, campaign.StartDate, campaign.EndDate);
    }
    public GetCampaignDto CreateCampaign(CreateCampaignDto createCampaignDto)
    {
        Campaign campaign = new(0, createCampaignDto.Name, createCampaignDto.Description, createCampaignDto.FundraisingGoal, 0.0, createCampaignDto.StartDate, createCampaignDto.EndDate);
        Campaign createdCampaign = campaignRepository.CreateCampaign(campaign);
        return new GetCampaignDto(createdCampaign.Id, createdCampaign.Name, createdCampaign.Description, createdCampaign.FundraisingGoal, createdCampaign.TotalRaised, createdCampaign.StartDate, createdCampaign.EndDate);
    }
    public GetCampaignDto UpdateCampaign(long id, UpdateCampaignDto updateCampaignDto)
    {
        Campaign campaign = new(id, updateCampaignDto.Name, updateCampaignDto.Description, updateCampaignDto.FundraisingGoal, 0.0, updateCampaignDto.StartDate, updateCampaignDto.EndDate);
        Campaign updatedCampaign = campaignRepository.UpdateCampaign(id, campaign);
        return new GetCampaignDto(updatedCampaign.Id, updatedCampaign.Name, updatedCampaign.Description, updatedCampaign.FundraisingGoal, updatedCampaign.TotalRaised, updatedCampaign.StartDate, updatedCampaign.EndDate);
    }
    public void DeleteCampaign(long id)
    {
        campaignRepository.DeleteCampaign(id);
    }
}
