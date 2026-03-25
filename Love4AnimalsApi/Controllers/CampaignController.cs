/*using Love4AnimalsApi.Dtos;
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
        public List<GetCampaignDto> GetCampaigns()
        {
            return this.campaignService.GetCampaigns();
        }
        [HttpGet("{id}")]
        public GetCampaignDto GetCampaign(long id)
        {
            return this.campaignService.GetCampaign(id);
        }
        [HttpPost("")]
        public GetCampaignDto CreateCampaign([FromBody] CreateCampaignDto createCampaignDto)
        {
            return this.campaignService.CreateCampaign(createCampaignDto);
        }
        [HttpPut("{id}")]
        public GetCampaignDto UpdateCampaign(long id, [FromBody] UpdateCampaignDto updateCampaignDto)
        {
            return this.campaignService.UpdateCampaign(id, updateCampaignDto);
        }
        [HttpDelete("{id}")]
        public void DeleteCampaign(long id)
        {
            this.campaignService.DeleteCampaign(id);
        }
    }
}
*/
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
        public List<GetCampaignDto> GetCampaigns()
        {
            return this.campaignService.GetCampaigns();
        }
        [HttpGet("{id}")]
        public GetCampaignDto GetCampaign(long id)
        {
            return this.campaignService.GetCampaign(id);
        }
        [HttpPost("")]
        public ActionResult<GetCampaignDto> CreateCampaign([FromBody] CreateCampaignDto createCampaignDto)
        {
            var campaign = this.campaignService.CreateCampaign(createCampaignDto);
            return CreatedAtAction(nameof(GetCampaign), new { id = campaign.Id }, campaign);
        }
        [HttpPut("{id}")]
        public GetCampaignDto UpdateCampaign(long id, [FromBody] UpdateCampaignDto updateCampaignDto)
        {
            return this.campaignService.UpdateCampaign(id, updateCampaignDto);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCampaign(long id)
        {
            this.campaignService.DeleteCampaign(id);
            return NoContent();
        }
    }
}