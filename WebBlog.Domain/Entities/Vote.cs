using WebBog.Domain.Common;
using WebBog.Domain.Entities;

namespace WebBlog.Domain.Entities;

public partial class Vote : BaseDomainEntity
{
    public string? UserId { get; set; }

    public string? PostId { get; set; }

    public int? VoteType { get; set; }

    public virtual Post? Post { get; set; }

    public virtual User? User { get; set; }
}
