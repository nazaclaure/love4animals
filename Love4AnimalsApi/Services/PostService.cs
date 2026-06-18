using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Love4AnimalsApi.Services;

public class PostService : IPostService
{
    private IPostRepository postRepository;
    private IUserRepository userRepository;
    private ICampaignRepository campaignRepository;
    private ICommentRepository commentRepository;
    private readonly IDistributedCache _cache;

    public PostService(IPostRepository postRepository, IUserRepository userRepository, ICampaignRepository campaignRepository, ICommentRepository commentRepository, IDistributedCache cache)
    {
        this.postRepository = postRepository;
        this.userRepository = userRepository;
        this.campaignRepository = campaignRepository;
        this.commentRepository = commentRepository;
        _cache = cache;
    }

    public List<GetPostDto> GetPosts()
    {
        var key = "posts:todos";
        try
        {
            var cached = _cache.GetString(key);
            if (cached != null)
                return JsonSerializer.Deserialize<List<GetPostDto>>(cached)!;
        }
        catch { }

        List<Post> posts = postRepository.GetPosts();
        var result = posts.Select(p => new GetPostDto(p.Id, p.Description, p.ImageURL, p.CreatedAt, p.UserId, p.CampaignId)).ToList();

        try
        {
            _cache.SetString(key, JsonSerializer.Serialize(result), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });
        }
        catch { }

        return result;
    }

    public GetPostDto? GetPost(long id)
    {
        Post? post = postRepository.GetPost(id);
        if (post == null) return null;
        return new GetPostDto(post.Id, post.Description, post.ImageURL, post.CreatedAt, post.UserId, post.CampaignId);
    }

    public GetPostDto? CreatePost(CreatePostDto createPostDto)
    {
        User? user = userRepository.GetUser(createPostDto.UserId);
        if (user == null) return null;
        Campaign? campaign = campaignRepository.GetCampaign(createPostDto.CampaignId);
        if (campaign == null) return null;
        Post post = new(0, createPostDto.Description, createPostDto.ImageURL, DateTime.Now, createPostDto.UserId, createPostDto.CampaignId);
        Post createdPost = postRepository.CreatePost(post);
        try { _cache.Remove("posts:todos"); } catch { }
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
        try { _cache.Remove("posts:todos"); } catch { }
        return new GetPostDto(updatedPost.Id, updatedPost.Description, updatedPost.ImageURL, updatedPost.CreatedAt, updatedPost.UserId, updatedPost.CampaignId);
    }

    public bool DeletePost(long id)
    {
        Post? post = postRepository.GetPost(id);
        if (post == null) return false;
        commentRepository.DeleteCommentsByPostId(id);
        try { _cache.Remove("posts:todos"); } catch { }
        return postRepository.DeletePost(id);
    }
}
