using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace Love4AnimalsApi.Services
{
    public class DonationService : IDonationService
    {
        private readonly IDonationRepository _repo;
        private readonly IUserService _userService;
        private readonly ICampaignService _campaignService;
        private readonly ICampaignRepository _campaignRepository;
        private readonly IDistributedCache _cache;

        public DonationService(IDonationRepository repo, IUserService userService, ICampaignService campaignService, ICampaignRepository campaignRepository, IDistributedCache cache)
        {
            _repo = repo;
            _userService = userService;
            _campaignService = campaignService;
            _campaignRepository = campaignRepository;
            _cache = cache;
        }

        public List<GetDonationDto> GetDonationsByCampaign(long campaignId)
        {
            var key = $"donations:campaign:{campaignId}";
            try
            {
                var cached = _cache.GetString(key);
                if (cached != null)
                    return JsonSerializer.Deserialize<List<GetDonationDto>>(cached)!;
            }
            catch { }

            var result = _repo.GetDonationsByCampaign(campaignId).Select(Map).ToList();

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

        public GetDonationDto? GetDonation(long id)
        {
            var d = _repo.GetDonation(id);
            return d == null ? null : Map(d);
        }

        public GetDonationDto CreateDonation(CreateDonationDto dto)
        {
            if (_userService.GetUser(dto.UserId) == null) throw new ArgumentException("User does not exist");
            var campaign = _campaignRepository.GetCampaign(dto.CampaignId);
            if (campaign == null) throw new ArgumentException("Campaign does not exist");

            var d = _repo.CreateDonation(new Donation {
                Amount = dto.Amount, Date = DateTime.UtcNow, Status = "Completed",
                UserId = dto.UserId, CampaignId = dto.CampaignId, Message = dto.Message
            });

            campaign.TotalRaised += (double)dto.Amount;
            _campaignRepository.UpdateCampaign(dto.CampaignId, campaign);

            try { _cache.Remove($"donations:campaign:{dto.CampaignId}"); } catch { }
            try { _cache.Remove("campaigns:todos"); } catch { }

            return Map(d);
        }

        public GetDonationDto? UpdateDonation(long id, UpdateDonationDto dto)
        {
            var existing = _repo.GetDonation(id);
            if (existing == null) return null;

            var oldAmount = existing.Amount;
            var campaign = _campaignRepository.GetCampaign(existing.CampaignId);
            if (campaign != null)
            {
                campaign.TotalRaised = campaign.TotalRaised - (double)oldAmount + (double)dto.Amount;
                _campaignRepository.UpdateCampaign(campaign.Id, campaign);
            }

            var updateData = new Donation {
                Amount = dto.Amount,
                Message = dto.Message,
                Status = dto.Status
            };
            var updated = _repo.UpdateDonation(id, updateData);
            if (updated == null) return null;

            try { _cache.Remove($"donations:campaign:{updated.CampaignId}"); } catch { }
            try { _cache.Remove("campaigns:todos"); } catch { }

            return Map(updated);
        }

        public bool DeleteDonation(long id)
        {
            var d = _repo.GetDonation(id);
            if (d != null)
            {
                var campaign = _campaignRepository.GetCampaign(d.CampaignId);
                if (campaign != null)
                {
                    campaign.TotalRaised -= (double)d.Amount;
                    _campaignRepository.UpdateCampaign(campaign.Id, campaign);
                }
                try { _cache.Remove($"donations:campaign:{d.CampaignId}"); } catch { }
                try { _cache.Remove("campaigns:todos"); } catch { }
            }
            return _repo.DeleteDonation(id);
        }

        private GetDonationDto Map(Donation d) => new GetDonationDto {
            Id = d.Id, Amount = d.Amount, Date = d.Date, Status = d.Status,
            UserId = d.UserId, CampaignId = d.CampaignId, Message = d.Message
        };
    }
}
