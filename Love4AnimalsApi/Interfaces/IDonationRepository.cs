using System.Collections.Generic;
using Love4AnimalsApi.Models;

namespace Love4AnimalsApi.Interfaces
{
    public interface IDonationRepository
    {
        List<Donation> GetDonationsByCampaign(long campaignId);
        Donation CreateDonation(Donation donation);
        
        // Solo pide campaña y usuario
        Donation? GetDonation(long campaignId, long userId);
        Donation? UpdateDonation(long campaignId, long userId, Donation donation);
        bool DeleteDonation(long campaignId, long userId);
    }
}