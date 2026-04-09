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
        [EndpointSummary("Get all donations for a campaign.")]
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
        [EndpointSummary("Create a new donation.")]
        [Consumes("application/json")]
        [ProducesResponseType<GetDonationDto>(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult CreateDonation([FromBody] CreateDonationDto dto)
        {
            try
            {
                var created = _donationService.CreateDonation(dto);
                return CreatedAtAction(nameof(GetDonation), new { campaignId = created.CampaignId, id = created.Id }, created);
            }
            catch (System.ArgumentException)
            {
                return NotFound();
            }
        }

        /// <summary>Get a donation by ID.</summary>
        /// <param name="campaignId">Campaign ID</param>
        /// <param name="id">Donation ID</param>
        [HttpGet("campaign/{campaignId}/{id}")]
        [EndpointSummary("Get a donation by ID.")]
        [ProducesResponseType<GetDonationDto>(200)]
        [ProducesResponseType(404)]
        public IActionResult GetDonation(long campaignId, long id)
        {
            var donation = _donationService.GetDonation(id);
            if (donation == null || donation.CampaignId != campaignId) return NotFound();
            return Ok(donation);
        }

        /// <summary>Update an existing donation.</summary>
        /// <param name="campaignId">Campaign ID</param>
        /// <param name="id">Donation ID</param>
        /// <param name="dto">Updated donation data</param>
        [HttpPut("campaign/{campaignId}/{id}")]
        [EndpointSummary("Update an existing donation.")]
        [Consumes("application/json")]
        [ProducesResponseType<GetDonationDto>(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDonation(long campaignId, long id, [FromBody] UpdateDonationDto dto)
        {
            var existing = _donationService.GetDonation(id);
            if (existing == null || existing.CampaignId != campaignId) return NotFound();
            var updated = _donationService.UpdateDonation(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        /// <summary>Delete a donation by ID.</summary>
        /// <param name="campaignId">Campaign ID</param>
        /// <param name="id">Donation ID</param>
        [HttpDelete("campaign/{campaignId}/{id}")]
        [EndpointSummary("Delete a donation by ID.")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDonation(long campaignId, long id)
        {
            var existing = _donationService.GetDonation(id);
            if (existing == null || existing.CampaignId != campaignId) return NotFound();
            var deleted = _donationService.DeleteDonation(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
