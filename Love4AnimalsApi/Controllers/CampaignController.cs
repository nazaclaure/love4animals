using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Love4AnimalsApi.Controllers
{
    [Route("v1/campaigns")]
    [ApiController]
    [Tags("Campaign")]
    public class CampaignController : ControllerBase
    {
        private ICampaignService campaignService;
        public CampaignController(ICampaignService campaignService)
        {
            this.campaignService = campaignService;
        }

        /// <summary>Get all campaigns.</summary>
        [HttpGet("")]
        [EndpointSummary("Get All Campaigns")]
        [ProducesResponseType<List<GetCampaignDto>>(200)]
        public ActionResult<List<GetCampaignDto>> GetCampaigns()
        {
            return Ok(this.campaignService.GetCampaigns());
        }

        /// <summary>Get a campaign by ID.</summary>
        [HttpGet("{id}")]
        [EndpointSummary("Get Campaign By Id")]
        [ProducesResponseType<GetCampaignDto>(200)]
        [ProducesResponseType(404)]
        public ActionResult<GetCampaignDto> GetCampaign(long id)
        {
            var campaign = this.campaignService.GetCampaign(id);
            if (campaign == null) return NotFound();
            return Ok(campaign);
        }

        /// <summary>Create a new campaign.</summary>
        [HttpPost("")]
        [EndpointSummary("Create Campaign")]
        [Consumes("application/json")]
        [ProducesResponseType<GetCampaignDto>(201)]
        [ProducesResponseType(400)]
        public ActionResult<GetCampaignDto> CreateCampaign([FromBody] CreateCampaignDto createCampaignDto)
        {
            var campaign = this.campaignService.CreateCampaign(createCampaignDto);
            return CreatedAtAction(nameof(GetCampaign), new { id = campaign.Id }, campaign);
        }

        /// <summary>Update an existing campaign.</summary>
        [HttpPut("{id}")]
        [EndpointSummary("Update Campaign")]
        [Consumes("application/json")]
        [ProducesResponseType<GetCampaignDto>(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public ActionResult<GetCampaignDto> UpdateCampaign(long id, [FromBody] UpdateCampaignDto updateCampaignDto)
        {
            var campaign = this.campaignService.UpdateCampaign(id, updateCampaignDto);
            if (campaign == null) return NotFound();
            return Ok(campaign);
        }

        /// <summary>Delete a campaign by ID.</summary>
        [HttpDelete("{id}")]
        [EndpointSummary("Delete Campaign")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCampaign(long id)
        {
            var result = this.campaignService.DeleteCampaign(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
