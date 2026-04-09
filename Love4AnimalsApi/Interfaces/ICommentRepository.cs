using Love4AnimalsApi.Models;
namespace Love4AnimalsApi.Interfaces;
public interface ICommentRepository
{
    List<Comment> GetComments(long postId);
    Comment? GetComment(long postId, long id);
    Comment CreateComment(Comment comment);
    Comment? UpdateComment(long id, Comment comment);
    bool DeleteComment(long id);
    void DeleteCommentsByPostId(long postId);
}
