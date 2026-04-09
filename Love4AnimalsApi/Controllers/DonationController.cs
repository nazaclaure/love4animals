using Microsoft.AspNetCore.Mvc;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Dtos;

namespace Love4AnimalsApi.Controllers
{
    /// <summary>Manage donations to campaigns.</summary>
    [ApiController]
    [Route("v1/donations")]
    [Tags("Donations")]
    [Produces("application/json")]
    public class DonationController : ControllerBase
    {
        private readonly IDonationService _donationService;

        public DonationController(IDonationService donationService)
        {
            _donationService = donationService;
        }

        /// <summary>Get all donations for a campaign.</summary>
        /// <param name="campaignId">Campaign ID</param>
        [HttpGet("campaign/{campaignId}")]
        [EndpointSummary("Get Donations By Campaign")]
        [ProducesResponseType<List<GetDonationDto>>(200)]
        [ProducesResponseType(404)]
        public IActionResult GetDonationsByCampaign(long campaignId)
        {
            var donations = _donationService.GetDonationsByCampaign(campaignId);
            if (donations == null || !donations.Any()) return NotFound();
            return Ok(donations);
        }

        /// <summary>Create a new donation.</summary>
        /// <param name="dto">Donation data</param>
        [HttpPost]
        [EndpointSummary("Create Donation")]
        [Consumes("application/json")]
        [ProducesResponseType<GetDonationDto>(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult CreateDonation([FromBody] CreateDonationDto dto)
        {
            try
            {
                var created = _donationService.CreateDonation(dto);
                return CreatedAtAction(nameof(GetDonationByCampaignAndUser), new { campaignId = created.CampaignId, userId = created.UserId }, created);
            }
            catch (System.ArgumentException)
            {
                return NotFound();
            }
        }

        /// <summary>Get a donation by campaign and user.</summary>
        /// <param name="campaignId">Campaign ID</param>
        /// <param name="userId">User ID</param>
        [HttpGet("campaign/{campaignId}/user/{userId}/donation")]
        [EndpointSummary("Get Donation By Campaign And User")]
        [ProducesResponseType<GetDonationDto>(200)]
        [ProducesResponseType(404)]
        public IActionResult GetDonationByCampaignAndUser(long campaignId, long userId)
        {
            var donation = _donationService.GetDonation(campaignId, userId);
            if (donation == null) return NotFound();
            return Ok(donation);
        }

        /// <summary>Update an existing donation.</summary>
        /// <param name="campaignId">Campaign ID</param>
        /// <param name="userId">User ID</param>
        /// <param name="dto">Updated donation data</param>
        [HttpPut("campaign/{campaignId}/user/{userId}/donation")]
        [EndpointSummary("Update Donation")]
        [Consumes("application/json")]
        [ProducesResponseType<GetDonationDto>(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDonation(long campaignId, long userId, [FromBody] UpdateDonationDto dto)
        {
            var updated = _donationService.UpdateDonation(campaignId, userId, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        /// <summary>Delete a donation by campaign and user.</summary>
        /// <param name="campaignId">Campaign ID</param>
        /// <param name="userId">User ID</param>
        [HttpDelete("campaign/{campaignId}/user/{userId}/donation")]
        [EndpointSummary("Delete Donation")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDonation(long campaignId, long userId)
        {
            var deleted = _donationService.DeleteDonation(campaignId, userId);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
