using WebBlog.Domain.Common;
using WebBlog.Domain.Entities;

namespace WebBlog.Domain.Entities;

public partial class Post : BaseDomainEntity
{
    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? Thumbnail { get; set; }

    public int? ViewCount { get; set; }

    public Guid? UserId { get; set; }

    public long? Timestamp { get; set; }

    public string? PostType { get; set; }

    public virtual List<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User? User { get; set; }

    public virtual List<Vote> Votes { get; set; } = new List<Vote>();

    public virtual List<Tag> Tags { get; set; } = new List<Tag>();
}
