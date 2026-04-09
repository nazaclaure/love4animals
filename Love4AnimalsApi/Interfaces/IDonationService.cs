using System.Collections.Generic;
using Love4AnimalsApi.Dtos;

namespace Love4AnimalsApi.Interfaces
{
    public interface IDonationService
    {
        List<GetDonationDto> GetDonations();
        GetDonationDto? GetDonation(long id);
        List<GetDonationDto> GetDonationsByCampaign(long campaignId);
        GetDonationDto CreateDonation(CreateDonationDto createDto);
        GetDonationDto? UpdateDonation(long id, UpdateDonationDto updateDto);
        bool DeleteDonation(long id);
    }
}
