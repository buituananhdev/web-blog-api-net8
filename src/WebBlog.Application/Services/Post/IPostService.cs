using WebBlog.Application.Dtos.Post;
using WebBlog.Domain.Payloads;

namespace WebBlog.Application.Services.Post
{
    public interface IPostService
    {
        Task<PaginatedResult<PostDto>> GetPaginationGetPopularPostsAsync(int page, int pageSize);
        Task<PostDto> CreatePostAsync(CreatePostDto post);
        Task<PostDto> GetPostAsync(Guid postID);
        Task<PostDto?> DeletePostAsync(Guid postID);
        Task<PostDto> UpdatePostAsync(Guid postID, PostDto postDto);
        Task IncrementViewCountPostAsync(Guid postID);
    }
}
