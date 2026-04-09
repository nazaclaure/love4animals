using System.Collections.Generic;
using Love4AnimalsApi.Dtos;

namespace Love4AnimalsApi.Interfaces
{
    public interface IDonationService
    {
        List<GetDonationDto> GetDonationsByCampaign(long campaignId);
        GetDonationDto CreateDonation(CreateDonationDto dto);
        
        GetDonationDto? GetDonation(long campaignId, long userId);
        GetDonationDto? UpdateDonation(long campaignId, long userId, UpdateDonationDto dto);
        bool DeleteDonation(long campaignId, long userId);
    }
}