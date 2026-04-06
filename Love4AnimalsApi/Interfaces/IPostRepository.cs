/*namespace Love4AnimalsApi.Interfaces;
public interface IPostRepository
{
    List<Models.Post> GetPosts();
    Models.Post GetPost(long id);
    Models.Post CreatePost(Models.Post post);
    Models.Post UpdatePost(long id, Models.Post post);
    void DeletePost(long id);
}*/
namespace Love4AnimalsApi.Interfaces;
public interface IPostRepository
{
    List<Models.Post> GetPosts();
    Models.Post GetPost(long id);
    Models.Post CreatePost(Models.Post post);
    Models.Post UpdatePost(long id, Models.Post post);
    bool DeletePost(long id);
}