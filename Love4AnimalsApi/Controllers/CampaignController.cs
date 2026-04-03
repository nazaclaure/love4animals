using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Love4AnimalsApi.Controllers
{
    [Route("v1/campaigns")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private ICampaignService campaignService;
        public CampaignController(ICampaignService campaignService)
        {
            this.campaignService = campaignService;
        }
        [HttpGet("")]
        public ActionResult<List<GetCampaignDto>> GetCampaigns()
        {
            return Ok(this.campaignService.GetCampaigns());
        }
        [HttpGet("{id}")]
        public ActionResult<GetCampaignDto> GetCampaign(long id)
        {
            var campaign = this.campaignService.GetCampaign(id);
            if (campaign == null) return NotFound();
            return Ok(campaign);
        }
        [HttpPost("")]
        public ActionResult<GetCampaignDto> CreateCampaign([FromBody] CreateCampaignDto createCampaignDto)
        {
            var campaign = this.campaignService.CreateCampaign(createCampaignDto);
            return CreatedAtAction(nameof(GetCampaign), new { id = campaign.Id }, campaign);
        }
        [HttpPut("{id}")]
        public ActionResult<GetCampaignDto> UpdateCampaign(long id, [FromBody] UpdateCampaignDto updateCampaignDto)
        {
            var campaign = this.campaignService.UpdateCampaign(id, updateCampaignDto);
            if (campaign == null) return NotFound();
            return Ok(campaign);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCampaign(long id)
        {
            var result = this.campaignService.DeleteCampaign(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
