namespace Love4AnimalsApi.Models;
public class Post
{
    public Post(long id, string description, string imageURL, DateTime createdAt, long userId, long campaignId)
    {
        this.Id = id;
        this.Description = description;
        this.ImageURL = imageURL;
        this.CreatedAt = createdAt;
        this.UserId = userId;
        this.CampaignId = campaignId;
    }
    public long Id { get; set; }
    public string Description { get; set; }
    public string ImageURL { get; set; }
    public DateTime CreatedAt { get; set; }
    public long UserId { get; set; }
    public long CampaignId { get; set; }
}
