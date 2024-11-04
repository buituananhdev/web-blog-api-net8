using WebBlog.Domain.Common;
using WebBlog.Domain.Entities;

namespace WebBlog.Domain.Entities;

public partial class UserConversation : BaseDomainEntity
{
    public Guid? ConversationId { get; set; }

    public virtual Conversation? Conversation { get; set; }

    public virtual User? User { get; set; }
}
