using Love4AnimalsApi.Dtos;
namespace Love4AnimalsApi.Interfaces;
public interface ICommentService
{
    List<GetCommentDto> GetComments(long postId);
    GetCommentDto GetComment(long postId, long id);
    GetCommentDto CreateComment(long postId, CreateCommentDto createCommentDto);
    GetCommentDto UpdateComment(long id, UpdateCommentDto updateCommentDto);
    void DeleteComment(long id);
}
