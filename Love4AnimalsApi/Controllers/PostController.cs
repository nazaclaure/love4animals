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
        public List<GetPostDto> GetPosts()
        {
            return this.postService.GetPosts();
        }
        [HttpGet("{id}")]
        public GetPostDto GetPost(long id)
        {
            return this.postService.GetPost(id);
        }
        [HttpPost("")]
        public ActionResult<GetPostDto> CreatePost([FromBody] CreatePostDto createPostDto)
        {
            var post = this.postService.CreatePost(createPostDto);
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }
        [HttpPut("{id}")]
        public GetPostDto UpdatePost(long id, [FromBody] UpdatePostDto updatePostDto)
        {
            return this.postService.UpdatePost(id, updatePostDto);
        }
        [HttpDelete("{id}")]
        public IActionResult DeletePost(long id)
        {
            this.postService.DeletePost(id);
            return NoContent();
        }
    }
}
