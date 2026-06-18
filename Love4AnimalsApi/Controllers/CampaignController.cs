using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Love4AnimalsApi.Controllers
{
    [Route("v1/campaigns")]
    [ApiController]
    [Authorize]
    [Tags("Campaign")]
    [Produces("application/json")]
    public class CampaignController : ControllerBase
    {
        private ICampaignService campaignService;
        public CampaignController(ICampaignService campaignService)
        {
            this.campaignService = campaignService;
        }

        [HttpGet("")]
        [EndpointSummary("Get All Campaigns")]
        [ProducesResponseType<List<GetCampaignDto>>(200)]
        public ActionResult<List<GetCampaignDto>> GetCampaigns()
        {
            return Ok(this.campaignService.GetCampaigns());
        }

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
//solo misionero puede crear, actualizar o borrar campaña, user id del token var userid
        [HttpPost("")]
        [Authorize(Roles = "Misionero")]
        [EndpointSummary("Create Campaign")]
        [Consumes("application/json")]
        [ProducesResponseType<GetCampaignDto>(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public ActionResult<GetCampaignDto> CreateCampaign([FromBody] CreateCampaignDto createCampaignDto)
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            createCampaignDto.UserId = userId;
            var campaign = this.campaignService.CreateCampaign(createCampaignDto);
            if (campaign == null) return NotFound();
            return CreatedAtAction(nameof(GetCampaign), new { id = campaign.Id }, campaign);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Misionero")]
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

        [HttpDelete("{id}")]
        [Authorize(Roles = "Misionero")]
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
