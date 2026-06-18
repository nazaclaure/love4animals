namespace Love4AnimalsApi.Models;
public class Comment
{
    public long Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public long UserId { get; set; }
    public long PostId { get; set; }
}