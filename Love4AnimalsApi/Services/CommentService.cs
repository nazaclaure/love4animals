using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;
namespace Love4AnimalsApi.Services;
public class CommentService : ICommentService
{
    private ICommentRepository commentRepository;
    private IPostRepository postRepository;
    private IUserRepository userRepository;
    public CommentService(ICommentRepository commentRepository, IPostRepository postRepository, IUserRepository userRepository)
    {
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;
        this.userRepository = userRepository;
    }
    public List<GetCommentDto>? GetComments(long postId)
    {
        Post? post = postRepository.GetPost(postId);
        if (post == null) return null;
        List<Comment> comments = commentRepository.GetComments(postId);
        return comments.Select(c => new GetCommentDto(c.Id, c.Content, c.CreatedAt, c.UserId, c.PostId)).ToList();
    }
    public GetCommentDto? GetComment(long postId, long id)
    {
        Post? post = postRepository.GetPost(postId);
        if (post == null) return null;
        Comment? comment = commentRepository.GetComment(postId, id);
        if (comment == null) return null;
        return new GetCommentDto(comment.Id, comment.Content, comment.CreatedAt, comment.UserId, comment.PostId);
    }
    public GetCommentDto? CreateComment(long postId, CreateCommentDto createCommentDto)
    {
        Post? post = postRepository.GetPost(postId);
        if (post == null) return null;
        User? user = userRepository.GetUser(createCommentDto.UserId);
        if (user == null) return null;
        Comment comment = new(0, createCommentDto.Content, DateTime.Now, createCommentDto.UserId, postId);
        Comment createdComment = commentRepository.CreateComment(comment);
        return new GetCommentDto(createdComment.Id, createdComment.Content, createdComment.CreatedAt, createdComment.UserId, createdComment.PostId);
    }
    public GetCommentDto? UpdateComment(long postId, long id, UpdateCommentDto updateCommentDto)
    {
        Post? post = postRepository.GetPost(postId);
        if (post == null) return null;
        Comment comment = new(id, updateCommentDto.Content, DateTime.Now, 0, postId);
        Comment? updatedComment = commentRepository.UpdateComment(id, comment);
        if (updatedComment == null) return null;
        return new GetCommentDto(updatedComment.Id, updatedComment.Content, updatedComment.CreatedAt, updatedComment.UserId, updatedComment.PostId);
    }
    public bool DeleteComment(long postId, long id)
    {
        Post? post = postRepository.GetPost(postId);
        if (post == null) return false;
        Comment? comment = commentRepository.GetComment(postId, id);
        if (comment == null) return false;
        return commentRepository.DeleteComment(id);
    }
}