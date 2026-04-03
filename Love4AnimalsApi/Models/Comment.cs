namespace Love4AnimalsApi.Models;
public class Comment
{
    public Comment(long id, string content, DateTime createdAt, long userId, long postId)
    {
        this.Id = id;
        this.Content = content;
        this.CreatedAt = createdAt;
        this.UserId = userId;
        this.PostId = postId;
    }
    public long Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public long UserId { get; set; }
    public long PostId { get; set; }
}
