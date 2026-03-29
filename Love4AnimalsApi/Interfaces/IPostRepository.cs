using Love4AnimalsApi.Models;
namespace Love4AnimalsApi.Interfaces;
public interface IPostRepository
{
    List<Post> GetPosts();
    Post GetPost(long id);
    Post CreatePost(Post post);
    Post UpdatePost(long id, Post post);
    void DeletePost(long id);
}
