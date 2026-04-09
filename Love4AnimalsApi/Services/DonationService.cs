using System;
using System.Collections.Generic;
using System.Linq;
using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;

namespace Love4AnimalsApi.Services
{
    public class DonationService : IDonationService
    {
        private readonly IDonationRepository _repo;
        private readonly IUserService _userService;
        private readonly ICampaignService _campaignService;

        public DonationService(IDonationRepository repo, IUserService userService, ICampaignService campaignService)
        {
            _repo = repo;
            _userService = userService;
            _campaignService = campaignService;
        }

        public List<GetDonationDto> GetDonationsByCampaign(long campaignId) => 
            _repo.GetDonationsByCampaign(campaignId).Select(Map).ToList();

        public GetDonationDto? GetDonation(long campaignId, long userId) 
        { 
            var d = _repo.GetDonation(campaignId, userId); 
            return d == null ? null : Map(d); 
        }

        public GetDonationDto CreateDonation(CreateDonationDto dto)
        {
            if (_userService.GetUser(dto.UserId) == null) throw new ArgumentException("Usuario no existe");
            if (_campaignService.GetCampaign(dto.CampaignId) == null) throw new ArgumentException("Campaña no existe");

            var d = _repo.CreateDonation(new Donation {
                Amount = dto.Amount, Date = DateTime.UtcNow, Status = "Completed",
                UserId = dto.UserId, CampaignId = dto.CampaignId, Message = dto.Message
            });
            return Map(d);
        }

       public GetDonationDto? UpdateDonation(long campaignId, long userId, UpdateDonationDto dto)
{
    var updateData = new Donation 
    { 
        Amount = dto.Amount, 
        Message = dto.Message,
        Status = dto.Status // 👈 Pasa el estado del DTO al Modelo
    };
    
    var updatedDonation = _repo.UpdateDonation(campaignId, userId, updateData);
    
    if (updatedDonation == null) return null;
    return Map(updatedDonation);
}

        public bool DeleteDonation(long campaignId, long userId) => 
            _repo.DeleteDonation(campaignId, userId);
        
        private GetDonationDto Map(Donation d) => new GetDonationDto {
            Id = d.Id, Amount = d.Amount, Date = d.Date, Status = d.Status, 
            UserId = d.UserId, CampaignId = d.CampaignId, Message = d.Message 
        };
    }
}