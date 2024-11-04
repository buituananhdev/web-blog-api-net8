using WebBlog.Domain.Common;

namespace WebBlog.Domain.Entities;

public partial class Tag : BaseDomainEntity
{
    public string Name { get; set; } = null!;

    public virtual List<Post> Posts { get; set; } = new List<Post>();
}
