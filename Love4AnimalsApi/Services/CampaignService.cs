using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Love4AnimalsApi.Services;

public class CampaignService : ICampaignService
{
    private ICampaignRepository campaignRepository;
    private IUserRepository userRepository;
    private readonly IDistributedCache _cache;

    public CampaignService(ICampaignRepository campaignRepository, IUserRepository userRepository, IDistributedCache cache)
    {
        this.campaignRepository = campaignRepository;
        this.userRepository = userRepository;
        _cache = cache;
    }

    public List<GetCampaignDto> GetCampaigns()
    {
        var key = "campaigns:todos";
        try
        {
            var cached = _cache.GetString(key);
            if (cached != null)
                return JsonSerializer.Deserialize<List<GetCampaignDto>>(cached)!;
        }
        catch { }

        List<Campaign> campaigns = campaignRepository.GetCampaigns();
        var result = campaigns.Select(c => new GetCampaignDto(c.Id, c.Name, c.Description, c.FundraisingGoal, c.TotalRaised, c.StartDate, c.EndDate, c.UserId)).ToList();

        try
        {
            _cache.SetString(key, JsonSerializer.Serialize(result), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });
        }
        catch { }

        return result;
    }

    public GetCampaignDto? GetCampaign(long id)
    {
        Campaign? campaign = campaignRepository.GetCampaign(id);
        if (campaign == null) return null;
        return new GetCampaignDto(campaign.Id, campaign.Name, campaign.Description, campaign.FundraisingGoal, campaign.TotalRaised, campaign.StartDate, campaign.EndDate, campaign.UserId);
    }

    public GetCampaignDto? CreateCampaign(CreateCampaignDto dto)
    {
        User? user = userRepository.GetUser(dto.UserId);
        if (user == null) return null;
        var startDate = DateTime.SpecifyKind(dto.StartDate, DateTimeKind.Utc);
        var endDate = DateTime.SpecifyKind(dto.EndDate, DateTimeKind.Utc);
        Campaign campaign = new(0, dto.Name, dto.Description, dto.FundraisingGoal, 0.0, startDate, endDate, dto.UserId);
        Campaign createdCampaign = campaignRepository.CreateCampaign(campaign);
        try { _cache.Remove("campaigns:todos"); } catch { }
        return new GetCampaignDto(createdCampaign.Id, createdCampaign.Name, createdCampaign.Description, createdCampaign.FundraisingGoal, createdCampaign.TotalRaised, createdCampaign.StartDate, createdCampaign.EndDate, createdCampaign.UserId);
    }

    public GetCampaignDto? UpdateCampaign(long id, UpdateCampaignDto dto)
    {
        Campaign? existing = campaignRepository.GetCampaign(id);
        if (existing == null) return null;
        var startDate = DateTime.SpecifyKind(dto.StartDate, DateTimeKind.Utc);
        var endDate = DateTime.SpecifyKind(dto.EndDate, DateTimeKind.Utc);
        existing.Name = dto.Name;
        existing.Description = dto.Description;
        existing.FundraisingGoal = dto.FundraisingGoal;
        existing.StartDate = startDate;
        existing.EndDate = endDate;
        Campaign? updatedCampaign = campaignRepository.UpdateCampaign(id, existing);
        if (updatedCampaign == null) return null;
        try { _cache.Remove("campaigns:todos"); } catch { }
        return new GetCampaignDto(updatedCampaign.Id, updatedCampaign.Name, updatedCampaign.Description, updatedCampaign.FundraisingGoal, updatedCampaign.TotalRaised, updatedCampaign.StartDate, updatedCampaign.EndDate, updatedCampaign.UserId);
    }

    public bool DeleteCampaign(long id)
    {
        try { _cache.Remove("campaigns:todos"); } catch { }
        return campaignRepository.DeleteCampaign(id);
    }
}
