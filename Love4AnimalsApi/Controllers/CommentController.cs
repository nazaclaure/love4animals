using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Love4AnimalsApi.Controllers
{
    [Route("v1/posts/{postId}/comments")]
    [ApiController]
    [Tags("Comment")]
    public class CommentController : ControllerBase
    {
        private ICommentService commentService;
        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        /// <summary>Retorna todos los comentarios de un post.</summary>
        [HttpGet("")]
        [EndpointSummary("Get All Comments")]
        [ProducesResponseType<List<GetCommentDto>>(200)]
        [ProducesResponseType(404)]
        public ActionResult<List<GetCommentDto>> GetComments(long postId)
        {
            var comments = this.commentService.GetComments(postId);
            if (comments == null) return NotFound();
            return Ok(comments);
        }

        /// <summary>Retorna un comentario por su ID.</summary>
        [HttpGet("{id}")]
        [EndpointSummary("Get Comment By Id")]
        [ProducesResponseType<GetCommentDto>(200)]
        [ProducesResponseType(404)]
        public ActionResult<GetCommentDto> GetComment(long postId, long id)
        {
            var comment = this.commentService.GetComment(postId, id);
            if (comment == null) return NotFound();
            return Ok(comment);
        }

        /// <summary>Crea un nuevo comentario en un post.</summary>
        [HttpPost("")]
        [EndpointSummary("Create Comment")]
        [Consumes("application/json")]
        [ProducesResponseType<GetCommentDto>(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public ActionResult<GetCommentDto> CreateComment(long postId, [FromBody] CreateCommentDto createCommentDto)
        {
            var comment = this.commentService.CreateComment(postId, createCommentDto);
            if (comment == null) return NotFound();
            return CreatedAtAction(nameof(GetComment), new { postId = postId, id = comment.Id }, comment);
        }

        /// <summary>Actualiza un comentario existente.</summary>
        [HttpPut("{id}")]
        [EndpointSummary("Update Comment")]
        [Consumes("application/json")]
        [ProducesResponseType<GetCommentDto>(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public ActionResult<GetCommentDto> UpdateComment(long postId, long id, [FromBody] UpdateCommentDto updateCommentDto)
        {
            var comment = this.commentService.UpdateComment(postId, id, updateCommentDto);
            if (comment == null) return NotFound();
            return Ok(comment);
        }

        /// <summary>Elimina un comentario por su ID.</summary>
        [HttpDelete("{id}")]
        [EndpointSummary("Delete Comment")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteComment(long postId, long id)
        {
            var result = this.commentService.DeleteComment(postId, id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
