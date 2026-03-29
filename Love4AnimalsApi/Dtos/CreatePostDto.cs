namespace Love4AnimalsApi.Dtos;
public class CreatePostDto
{
    public string Description { get; set; }
    public string ImageURL { get; set; }
    public long UserId { get; set; }
    public long CampaignId { get; set; }
}
