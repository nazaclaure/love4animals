using System.Collections.Generic;
using Love4AnimalsApi.Models;

namespace Love4AnimalsApi.Interfaces
{
    public interface IDonationRepository
    {
        List<Donation> GetDonations();
        Donation? GetDonation(long id);
        List<Donation> GetDonationsByCampaign(long campaignId);
        Donation CreateDonation(Donation donation);
        Donation? UpdateDonation(Donation donation);
        bool DeleteDonation(long id);
    }
}
