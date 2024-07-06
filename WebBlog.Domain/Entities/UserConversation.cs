using WebBog.Domain.Common;
using WebBog.Domain.Entities;

namespace WebBlog.Domain.Entities;

public partial class UserConversation : BaseDomainEntity
{
    public string? ConversationId { get; set; }

    public virtual Conversation? Conversation { get; set; }

    public virtual User? User { get; set; }
}
