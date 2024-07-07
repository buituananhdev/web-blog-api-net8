using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WebBlog.Application.Dtos;
using WebBlog.Domain.Entities;
using WebBlog.Service.Services.CommentService;

namespace WebBlog.Api.Controllers
{
    [ApiController]
    [Route("comments")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _CommentService;
        public CommentsController(ICommentService CommentService) => _CommentService = CommentService;

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<List<CommentDto>>> AddComment(CommentDto comment)
        {
            var result = await _CommentService.AddComment(comment);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Comment>>> GetCommentsForPost(Guid id, int page, int pageSize)
        {
            var result = await _CommentService.GetCommentsForPost(id, page, pageSize);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Comment>>> DeleteComment(Guid id)
        {
            var result = await _CommentService.DeleteComment(id);
            return Ok(result);
        }
    }
}
