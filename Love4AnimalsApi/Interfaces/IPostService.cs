using Love4AnimalsApi.Dtos;
namespace Love4AnimalsApi.Interfaces;
public interface IPostService
{
    List<GetPostDto> GetPosts();
    GetPostDto? GetPost(long id);
    GetPostDto CreatePost(CreatePostDto createPostDto);
    GetPostDto? UpdatePost(long id, UpdatePostDto updatePostDto);
    bool DeletePost(long id);
}
