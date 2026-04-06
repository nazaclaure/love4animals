/*using Love4AnimalsApi.Dtos;
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
        public ActionResult<List<GetCommentDto>> GetComments(long postId)
        {
            return Ok(this.commentService.GetComments(postId));
        }
        [HttpGet("{id}")]
        public ActionResult<GetCommentDto> GetComment(long postId, long id)
        {
            var comment = this.commentService.GetComment(postId, id);
            if (comment == null) return NotFound();
            return Ok(comment);
        }
        [HttpPost("")]
        public ActionResult<GetCommentDto> CreateComment(long postId, [FromBody] CreateCommentDto createCommentDto)
        {
            var comment = this.commentService.CreateComment(postId, createCommentDto);
            return CreatedAtAction(nameof(GetComment), new { postId = postId, id = comment.Id }, comment);
        }
        [HttpPut("{id}")]
        public ActionResult<GetCommentDto> UpdateComment(long postId, long id, [FromBody] UpdateCommentDto updateCommentDto)
        {
            var comment = this.commentService.UpdateComment(id, updateCommentDto);
            if (comment == null) return NotFound();
            return Ok(comment);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteComment(long postId, long id)
        {
            var result = this.commentService.DeleteComment(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
*/
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
        [EndpointSummary("Get All Comments")]
        public ActionResult<List<GetCommentDto>> GetComments(long postId)
        {
            return Ok(this.commentService.GetComments(postId));
        }

        [HttpGet("{id}")]
        [EndpointSummary("Get Comment By Id")]
        public ActionResult<GetCommentDto> GetComment(long postId, long id)
        {
            var comment = this.commentService.GetComment(postId, id);
            if (comment == null) return NotFound();
            return Ok(comment);
        }

        [HttpPost("")]
        [EndpointSummary("Create Comment")]
        public ActionResult<GetCommentDto> CreateComment(long postId, [FromBody] CreateCommentDto createCommentDto)
        {
            var comment = this.commentService.CreateComment(postId, createCommentDto);
            return CreatedAtAction(nameof(GetComment), new { postId = postId, id = comment.Id }, comment);
        }

        [HttpPut("{id}")]
        [EndpointSummary("Update Comment")]
        public ActionResult<GetCommentDto> UpdateComment(long postId, long id, [FromBody] UpdateCommentDto updateCommentDto)
        {
            var comment = this.commentService.UpdateComment(id, updateCommentDto);
            if (comment == null) return NotFound();
            return Ok(comment);
        }

        [HttpDelete("{id}")]
        [EndpointSummary("Delete Comment")]
        public IActionResult DeleteComment(long postId, long id)
        {
            var result = this.commentService.DeleteComment(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}