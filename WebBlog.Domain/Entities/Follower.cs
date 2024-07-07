using WebBog.Domain.Common;
using WebBog.Domain.Entities;

namespace WebBlog.Domain.Entities;
public partial class Follower : BaseDomainEntity
{

    public Guid? UserId { get; set; }

    public Guid? FollowerUserId { get; set; }

    public virtual User? FollowerUser { get; set; }

    public virtual User? User { get; set; }
}
