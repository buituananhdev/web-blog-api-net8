using WebBlog.Domain.Entities;
using WebBlog.Domain.Enums;
using WebBlog.Domain.Common;

namespace WebBlog.Domain.Entities
{
    public class User : BaseDomainEntity
    {
        public string? Fullname { get; set; } = string.Empty;   

        public string? Describe { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? Avatar { get; set; }

        public Status IsActive { get; set; } = Status.Inactive;
        public virtual List<Comment> Comments { get; set; } = new List<Comment>();

        public virtual List<Follower> FollowerFollowerUsers { get; set; } = new List<Follower>();

        public virtual List<Follower> FollowerUsers { get; set; } = new List<Follower>();

        public virtual List<Message> Messages { get; set; } = new List<Message>();

        public virtual List<Post> Posts { get; set; } = new List<Post>();

        public virtual List<Token> Tokens { get; set; } = new List<Token>();

        public virtual List<Vote> Votes { get; set; } = new List<Vote>();
    }
}
