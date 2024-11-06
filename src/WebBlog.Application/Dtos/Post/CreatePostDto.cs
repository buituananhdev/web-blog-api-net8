using WebBlog.Domain.Enums;

namespace WebBlog.Application.Dtos.Post
{
    public class CreatePostDto
    {
        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;

        public string? Thumbnail { get; set; }

        public PostType PostType { get; set; }
    }
}
