using Love4AnimalsApi.Dtos;
namespace Love4AnimalsApi.Interfaces;
public interface ICommentService
{
    public List<GetCommentDto>? GetComments(long postId);
    public GetCommentDto? GetComment(long postId, long id);
    public GetCommentDto? CreateComment(long postId, CreateCommentDto createCommentDto);
    public GetCommentDto? UpdateComment(long postId, long id, UpdateCommentDto updateCommentDto);
    public bool DeleteComment(long postId, long id);
}
