using Microsoft.AspNetCore.Mvc;
using WebBlog.Api.Params;
using Microsoft.AspNetCore.Authorization;
using WebBlog.Application.Services.User;
using WebBlog.Application.Services.Post;
using WebBlog.Application.Dtos.Post;

namespace WebBlog.Api.Controllers
{
    /// <summary>
    /// Controller for managing post-related operations.
    /// </summary>
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePostAsync([FromBody]CreatePostDto postDto)
        {
            var result = await _postService.CreatePostAsync(postDto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetPopularPostsAsync(PaginationParams paginationParams)
        {
            var result = await _postService.GetPaginationGetPopularPostsAsync(paginationParams.Page, paginationParams.PageSize);
            return Ok(result);
        }

        [HttpGet]
        [Route("{post_id}")]
        public async Task<IActionResult> GetPostAsync([FromRoute] Guid post_id)
        {
            var result = await _postService.GetPostAsync(post_id);
            return Ok(result);
        }
    }
}
