using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;
namespace Love4AnimalsApi.Services;
public class PostService : IPostService
{
    private IPostRepository postRepository;
    public PostService(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }
    public List<GetPostDto> GetPosts()
    {
        List<Post> posts = postRepository.GetPosts();
        return posts.Select(p => new GetPostDto(p.Id, p.Description, p.ImageURL, p.CreatedAt, p.UserId, p.CampaignId)).ToList();
    }
    public GetPostDto? GetPost(long id)
    {
        Post? post = postRepository.GetPost(id);
        if (post == null) return null;
        return new GetPostDto(post.Id, post.Description, post.ImageURL, post.CreatedAt, post.UserId, post.CampaignId);
    }
    public GetPostDto CreatePost(CreatePostDto createPostDto)
    {
        Post post = new(0, createPostDto.Description, createPostDto.ImageURL, DateTime.Now, createPostDto.UserId, createPostDto.CampaignId);
        Post createdPost = postRepository.CreatePost(post);
        return new GetPostDto(createdPost.Id, createdPost.Description, createdPost.ImageURL, createdPost.CreatedAt, createdPost.UserId, createdPost.CampaignId);
    }
    public GetPostDto? UpdatePost(long id, UpdatePostDto updatePostDto)
    {
        Post? post = postRepository.GetPost(id);
        if (post == null) return null;
        post.Description = updatePostDto.Description;
        post.ImageURL = updatePostDto.ImageURL;
        post.CampaignId = updatePostDto.CampaignId;
        Post? updatedPost = postRepository.UpdatePost(id, post);
        if (updatedPost == null) return null;
        return new GetPostDto(updatedPost.Id, updatedPost.Description, updatedPost.ImageURL, updatedPost.CreatedAt, updatedPost.UserId, updatedPost.CampaignId);
    }
    public bool DeletePost(long id)
    {
        return postRepository.DeletePost(id);
    }
}
