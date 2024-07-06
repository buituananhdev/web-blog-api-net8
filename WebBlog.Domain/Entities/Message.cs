using WebBog.Domain.Common;
using WebBog.Domain.Entities;

namespace WebBlog.Domain.Entities;

public partial class Message : BaseDomainEntity
{
    public string Content { get; set; } = null!;

    public string SenderId { get; set; } = null!;

    public string ConversationId { get; set; } = null!;

    public virtual Conversation Conversation { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
