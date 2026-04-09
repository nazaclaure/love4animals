using Microsoft.AspNetCore.Mvc;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Dtos;

namespace Love4AnimalsApi.Controllers
{
    [ApiController]
    [Route("v1/donations")] // <--- ¡AQUÍ ESTÁ EL CAMBIO CLAVE!
    [Tags("Donations")]
    public class DonationController : ControllerBase
    {
        private readonly IDonationService _donationService;

        public DonationController(IDonationService donationService)
        {
            _donationService = donationService;
        }

        /// <summary>
        /// Get All Donations
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetDonationDto>), StatusCodes.Status200OK)]
        public IActionResult GetDonations()
        {
            return Ok(_donationService.GetDonations());
        }

        /// <summary>
        /// Create a Donation
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(GetDonationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CreateDonation([FromBody] CreateDonationDto dto)
        {
            try
            {
                var created = _donationService.CreateDonation(dto);
                return CreatedAtAction(nameof(GetDonation), new { id = created.Id }, created);
            }
            catch (System.ArgumentException)
            {
                // Si el servicio falla (ej. no existe usuario o campaña), damos 404 directo
                return NotFound(); 
            }
        }

        /// <summary>
        /// Get Donation by ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetDonationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetDonation(long id)
        {
            var donation = _donationService.GetDonation(id);
            if (donation == null) return NotFound();
            return Ok(donation);
        }

        /// <summary>
        /// Update Donation by ID
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(GetDonationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateDonation(long id, [FromBody] UpdateDonationDto dto)
        {
            try 
            {
                var updated = _donationService.UpdateDonation(id, dto);
                if (updated == null) return NotFound();
                
                return Ok(updated);
            }
            catch (System.ArgumentException)
            {
                // Igual aquí, si hay error de IDs, damos 404 y no nos complicamos
                return NotFound();
            }
        }

        /// <summary>
        /// Delete Donation by ID
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteDonation(long id)
        {
            var deleted = _donationService.DeleteDonation(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Get Donations by Campaign ID
        /// </summary>
        [HttpGet("campaign/{campaignId}")]
        [ProducesResponseType(typeof(List<GetDonationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByCampaign(long campaignId)
        {
            var donations = _donationService.GetDonationsByCampaign(campaignId);
            if (donations == null || !donations.Any()) return NotFound();
            return Ok(donations);
        }
    }
}