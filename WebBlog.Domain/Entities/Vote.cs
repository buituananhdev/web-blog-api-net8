using WebBlog.Domain.Common;
using WebBlog.Domain.Entities;

namespace WebBlog.Domain.Entities;

public partial class Vote : BaseDomainEntity
{
    public Guid? UserId { get; set; }

    public Guid? PostId { get; set; }

    public int? VoteType { get; set; }

    public virtual Post? Post { get; set; }

    public virtual User? User { get; set; }
}
