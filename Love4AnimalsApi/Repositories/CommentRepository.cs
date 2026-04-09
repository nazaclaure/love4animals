using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;
namespace Love4AnimalsApi.Repositories;
public class CommentRepository : ICommentRepository
{
    private List<Comment> Comments { get; set; }
    public CommentRepository()
    {
        this.Comments = [];
        Comment newComment = new(1, "Great campaign!", DateTime.Now, 1, 1);
        this.Comments.Add(newComment);
    }
    public List<Comment> GetComments(long postId)
    {
        return this.Comments.Where(c => c.PostId == postId).ToList();
    }
    public Comment? GetComment(long postId, long id)
    {
        return this.Comments.FirstOrDefault(c => c.PostId == postId && c.Id == id);
    }
    public Comment CreateComment(Comment comment)
    {
        comment.Id = this.Comments.Any() ? this.Comments.Max(c => c.Id) + 1 : 1;
        this.Comments.Add(comment);
        return comment;
    }
    public Comment? UpdateComment(long id, Comment comment)
    {
        Comment? existingComment = this.Comments.FirstOrDefault(c => c.Id == id);
        if (existingComment == null) return null;
        existingComment.Content = comment.Content;
        return existingComment;
    }
    public bool DeleteComment(long id)
    {
        Comment? existingComment = this.Comments.FirstOrDefault(c => c.Id == id);
        if (existingComment == null) return false;
        this.Comments.Remove(existingComment);
        return true;
    }
    public void DeleteCommentsByPostId(long postId)
    {
        this.Comments.RemoveAll(c => c.PostId == postId);
    }
}
