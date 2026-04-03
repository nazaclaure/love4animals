using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Love4AnimalsApi.Controllers
{
    [Route("v1/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private IPostService postService;
        public PostController(IPostService postService)
        {
            this.postService = postService;
        }
        [HttpGet("")]
        public ActionResult<List<GetPostDto>> GetPosts()
        {
            return Ok(this.postService.GetPosts());
        }
        [HttpGet("{id}")]
        public ActionResult<GetPostDto> GetPost(long id)
        {
            var post = this.postService.GetPost(id);
            if (post == null) return NotFound();
            return Ok(post);
        }
        [HttpPost("")]
        public ActionResult<GetPostDto> CreatePost([FromBody] CreatePostDto createPostDto)
        {
            var post = this.postService.CreatePost(createPostDto);
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }
        [HttpPut("{id}")]
        public ActionResult<GetPostDto> UpdatePost(long id, [FromBody] UpdatePostDto updatePostDto)
        {
            var post = this.postService.UpdatePost(id, updatePostDto);
            if (post == null) return NotFound();
            return Ok(post);
        }
        [HttpDelete("{id}")]
        public IActionResult DeletePost(long id)
        {
            var result = this.postService.DeletePost(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
