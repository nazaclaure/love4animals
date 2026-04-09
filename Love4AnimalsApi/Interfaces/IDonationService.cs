using System.Collections.Generic;
using Love4AnimalsApi.Dtos;

namespace Love4AnimalsApi.Interfaces
{
    public interface IDonationService
    {
        List<GetDonationDto> GetDonationsByCampaign(long campaignId);
        GetDonationDto? GetDonation(long id);
        GetDonationDto CreateDonation(CreateDonationDto dto);
        GetDonationDto? UpdateDonation(long id, UpdateDonationDto dto);
        bool DeleteDonation(long id);
    }
}
