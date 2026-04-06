/*using Love4AnimalsApi.Dtos;
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
}*/
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
        [EndpointSummary("Get All Posts")]
        public ActionResult<List<GetPostDto>> GetPosts()
        {
            return Ok(this.postService.GetPosts());
        }

        [HttpGet("{id}")]
        [EndpointSummary("Get Post By Id")]
        public ActionResult<GetPostDto> GetPost(long id)
        {
            var post = this.postService.GetPost(id);
            if (post == null) return NotFound();
            return Ok(post);
        }

        [HttpPost("")]
        [EndpointSummary("Create Post")]
        public ActionResult<GetPostDto> CreatePost([FromBody] CreatePostDto createPostDto)
        {
            var post = this.postService.CreatePost(createPostDto);
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        [EndpointSummary("Update Post")]
        public ActionResult<GetPostDto> UpdatePost(long id, [FromBody] UpdatePostDto updatePostDto)
        {
            var post = this.postService.UpdatePost(id, updatePostDto);
            if (post == null) return NotFound();
            return Ok(post);
        }

        [HttpDelete("{id}")]
        [EndpointSummary("Delete Post")]
        public IActionResult DeletePost(long id)
        {
            this.postService.DeletePost(id);
            return NoContent();
        }
    }
}