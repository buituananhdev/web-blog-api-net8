using WebBlog.Application.Dtos;
using WebBlog.Domain.Entities;
using WebBlog.Domain.Payloads;

namespace WebBlog.Service.Services.CommentService
{
    public interface ICommentService
    {
        Task<CommentDto> AddComment(CommentDto comment);
        Task<CommentDto> DeleteComment(Guid commentID);
        Task<PaginatedResult<CommentDto>> GetCommentsForPost(Guid postId, int page, int pageSize);
    }
}
