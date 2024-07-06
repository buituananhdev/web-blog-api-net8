﻿using WebBlog.Domain.Entities;
using WebBog.Domain.Common;

namespace WebBog.Domain.Entities
{
    public class User : BaseDomainEntity
    {
        public string Fullname { get; set; } = null!;   

        public string? Describe { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? Avatar { get; set; }

        public int? IsActive { get; set; }
        public virtual List<Comment> Comments { get; set; } = new List<Comment>();

        public virtual List<Follower> FollowerFollowerUsers { get; set; } = new List<Follower>();

        public virtual List<Follower> FollowerUsers { get; set; } = new List<Follower>();

        public virtual List<Message> Messages { get; set; } = new List<Message>();

        public virtual List<Post> Posts { get; set; } = new List<Post>();

        public virtual List<Token> Tokens { get; set; } = new List<Token>();

        public virtual List<Vote> Votes { get; set; } = new List<Vote>();
    }
}
