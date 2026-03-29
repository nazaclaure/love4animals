using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;
namespace Love4AnimalsApi.Repositories;
public class PostRepository : IPostRepository
{
    private List<Post> Posts { get; set; }
    public PostRepository()
    {
        this.Posts = [];
        Post newPost = new(1, "Save the bears post", "https://picsum.photos/300", DateTime.Now, 1, 1);
        this.Posts.Add(newPost);
    }
    public List<Post> GetPosts()
    {
        return this.Posts;
    }
    public Post GetPost(long id)
    {
        return this.Posts.First(p => p.Id == id);
    }
    public Post CreatePost(Post post)
    {
        post.Id = this.Posts.Max(p => p.Id) + 1;
        this.Posts.Add(post);
        return post;
    }
    public Post UpdatePost(long id, Post post)
    {
        Post existingPost = this.Posts.First(p => p.Id == id);
        existingPost.Description = post.Description;
        existingPost.ImageURL = post.ImageURL;
        existingPost.CampaignId = post.CampaignId;
        return existingPost;
    }
    public void DeletePost(long id)
    {
        Post existingPost = this.Posts.First(p => p.Id == id);
        this.Posts.Remove(existingPost);
    }
}
