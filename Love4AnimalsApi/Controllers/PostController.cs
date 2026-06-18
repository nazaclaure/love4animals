using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Love4AnimalsApi.Controllers
{
    [Route("v1/posts")]
    [ApiController]
    [Authorize]
    [Tags("Post")]
    [Produces("application/json")]
    public class PostController : ControllerBase
    {
        private IPostService postService;
        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet("")]
        [EndpointSummary("Get All Posts")]
        [ProducesResponseType<List<GetPostDto>>(200)]
        public ActionResult<List<GetPostDto>> GetPosts()
        {
            return Ok(this.postService.GetPosts());
        }

        [HttpGet("{id}")]
        [EndpointSummary("Get Post By Id")]
        [ProducesResponseType<GetPostDto>(200)]
        [ProducesResponseType(404)]
        public ActionResult<GetPostDto> GetPost(long id)
        {
            var post = this.postService.GetPost(id);
            if (post == null) return NotFound();
            return Ok(post);
        }
//solo misionero puede crear, actualizar o borrar post, user id del token con var userid
        [HttpPost("")]
        [Authorize(Roles = "Misionero")]
        [EndpointSummary("Create Post")]
        [Consumes("application/json")]
        [ProducesResponseType<GetPostDto>(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public ActionResult<GetPostDto> CreatePost([FromBody] CreatePostDto createPostDto)
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            createPostDto.UserId = userId;
            var post = this.postService.CreatePost(createPostDto);
            if (post == null) return NotFound();
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Misionero")]
        [EndpointSummary("Update Post")]
        [Consumes("application/json")]
        [ProducesResponseType<GetPostDto>(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public ActionResult<GetPostDto> UpdatePost(long id, [FromBody] UpdatePostDto updatePostDto)
        {
            var post = this.postService.UpdatePost(id, updatePostDto);
            if (post == null) return NotFound();
            return Ok(post);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Misionero")]
        [EndpointSummary("Delete Post")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePost(long id)
        {
            var result = this.postService.DeletePost(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
