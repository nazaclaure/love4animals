using System.Collections.Generic;
using Love4AnimalsApi.Models;

namespace Love4AnimalsApi.Interfaces
{
    public interface IDonationRepository
    {
        List<Donation> GetDonationsByCampaign(long campaignId);
        Donation? GetDonation(long id);
        Donation CreateDonation(Donation donation);
        Donation? UpdateDonation(long id, Donation donation);
        bool DeleteDonation(long id);
    }
}
