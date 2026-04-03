using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Love4AnimalsApi.Controllers
{
    [Route("v1/posts/{postId}/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ICommentService commentService;
        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }
        [HttpGet("")]
        public List<GetCommentDto> GetComments(long postId)
        {
            return this.commentService.GetComments(postId);
        }
        [HttpGet("{id}")]
        public GetCommentDto GetComment(long postId, long id)
        {
            return this.commentService.GetComment(postId, id);
        }
        [HttpPost("")]
        public ActionResult<GetCommentDto> CreateComment(long postId, [FromBody] CreateCommentDto createCommentDto)
        {
            var comment = this.commentService.CreateComment(postId, createCommentDto);
            return CreatedAtAction(nameof(GetComment), new { postId = postId, id = comment.Id }, comment);
        }
        [HttpPut("{id}")]
        public GetCommentDto UpdateComment(long postId, long id, [FromBody] UpdateCommentDto updateCommentDto)
        {
            return this.commentService.UpdateComment(id, updateCommentDto);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteComment(long postId, long id)
        {
            this.commentService.DeleteComment(id);
            return NoContent();
        }
    }
}
