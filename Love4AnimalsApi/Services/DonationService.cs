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

        public List<GetDonationDto> GetDonations() => _repo.GetDonations().Select(Map).ToList();
        public GetDonationDto? GetDonation(long id) { var d = _repo.GetDonation(id); return d == null ? null : Map(d); }
        public List<GetDonationDto> GetDonationsByCampaign(long id) => _repo.GetDonationsByCampaign(id).Select(Map).ToList();

        public GetDonationDto CreateDonation(CreateDonationDto dto)
        {
            if (_userService.GetUser(dto.UserId) == null) throw new ArgumentException("Usuario no existe");
            if (_campaignService.GetCampaign(dto.CampaignId) == null) throw new ArgumentException("Campaña no existe");

            var d = _repo.CreateDonation(new Donation {
                Amount = dto.Amount, Date = DateTime.UtcNow, Status = "Completed",
                UserId = dto.UserId, CampaignId = dto.CampaignId
            });
            return Map(d);
        }

        public GetDonationDto? UpdateDonation(long id, UpdateDonationDto dto)
        {
            var d = _repo.GetDonation(id);
            if (d == null) return null;
            d.Amount = dto.Amount; d.Status = dto.Status;
            return Map(_repo.UpdateDonation(d)!);
        }

        public bool DeleteDonation(long id) => _repo.DeleteDonation(id);
        private GetDonationDto Map(Donation d) => new GetDonationDto {
            Id = d.Id, Amount = d.Amount, Date = d.Date, Status = d.Status, UserId = d.UserId, CampaignId = d.CampaignId
        };
    }
}
