using WebBlog.Domain.Common;
using WebBlog.Domain.Entities;

namespace WebBlog.Domain.Entities;
public partial class Comment : BaseDomainEntity
{

    public string CommentText { get; set; } = null!;

    public Guid? UserId { get; set; }

    public Guid? PostId { get; set; }

    public long? Timestamp { get; set; }

    public string? CommentType { get; set; }

    public virtual Post? Post { get; set; }

    public virtual User? User { get; set; }
}
