using WebBlog.Domain.Common;
using WebBlog.Domain.Entities;

namespace WebBlog.Domain.Entities;

public partial class Message : BaseDomainEntity
{
    public string Content { get; set; } = null!;

    public Guid? SenderId { get; set; } = null!;

    public Guid? ConversationId { get; set; } = null!;

    public virtual Conversation Conversation { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
