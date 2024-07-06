using WebBog.Domain.Common;

namespace WebBlog.Domain.Entities;

public partial class Conversation : BaseDomainEntity
{

    public string? ConversationName { get; set; }

    public virtual List<Message> Messages { get; set; } = new List<Message>();
}
