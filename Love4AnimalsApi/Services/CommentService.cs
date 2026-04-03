using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;
namespace Love4AnimalsApi.Services;
public class CommentService : ICommentService
{
    private ICommentRepository commentRepository;
    public CommentService(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }
    public List<GetCommentDto> GetComments(long postId)
    {
        List<Comment> comments = commentRepository.GetComments(postId);
        return comments.Select(c => new GetCommentDto(c.Id, c.Content, c.CreatedAt, c.UserId, c.PostId)).ToList();
    }
    public GetCommentDto GetComment(long postId, long id)
    {
        Comment comment = commentRepository.GetComment(postId, id);
        return new GetCommentDto(comment.Id, comment.Content, comment.CreatedAt, comment.UserId, comment.PostId);
    }
    public GetCommentDto CreateComment(long postId, CreateCommentDto createCommentDto)
    {
        Comment comment = new(0, createCommentDto.Content, DateTime.Now, createCommentDto.UserId, postId);
        Comment createdComment = commentRepository.CreateComment(comment);
        return new GetCommentDto(createdComment.Id, createdComment.Content, createdComment.CreatedAt, createdComment.UserId, createdComment.PostId);
    }
    public GetCommentDto UpdateComment(long id, UpdateCommentDto updateCommentDto)
    {
        Comment comment = new(id, updateCommentDto.Content, DateTime.Now, 0, 0);
        Comment updatedComment = commentRepository.UpdateComment(id, comment);
        return new GetCommentDto(updatedComment.Id, updatedComment.Content, updatedComment.CreatedAt, updatedComment.UserId, updatedComment.PostId);
    }
    public void DeleteComment(long id)
    {
        commentRepository.DeleteComment(id);
    }
}
