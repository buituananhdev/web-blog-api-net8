using WebBlog.Domain.Common;
using WebBlog.Domain.Entities;

namespace WebBlog.Domain.Entities;

public partial class Token : BaseDomainEntity
{
    public string? Email { get; set; }

    public string? AccessToken { get; set; }

    public string? RefreshToken { get; set; }

    public long? ExpirationTime { get; set; }

    public long? CreatedAt { get; set; }

    public virtual User? EmailNavigation { get; set; }
}
