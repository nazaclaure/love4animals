using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Love4AnimalsApi.Controllers
{
    [Route("v1/posts")]
    [ApiController]
    [Tags("Post")]
    public class PostController : ControllerBase
    {
        private IPostService postService;
        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        /// <summary>Retorna todos los posts registrados.</summary>
        [HttpGet("")]
        [EndpointSummary("Get All Posts")]
        [ProducesResponseType<List<GetPostDto>>(200)]
        public ActionResult<List<GetPostDto>> GetPosts()
        {
            return Ok(this.postService.GetPosts());
        }

        /// <summary>Retorna un post por su ID.</summary>
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

        /// <summary>Crea un nuevo post.</summary>
        [HttpPost("")]
        [EndpointSummary("Create Post")]
        [Consumes("application/json")]
        [ProducesResponseType<GetPostDto>(201)]
        [ProducesResponseType(400)]
        public ActionResult<GetPostDto> CreatePost([FromBody] CreatePostDto createPostDto)
        {
            var post = this.postService.CreatePost(createPostDto);
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        /// <summary>Actualiza un post existente.</summary>
        [HttpPut("{id}")]
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

        /// <summary>Elimina un post por su ID.</summary>
        [HttpDelete("{id}")]
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
