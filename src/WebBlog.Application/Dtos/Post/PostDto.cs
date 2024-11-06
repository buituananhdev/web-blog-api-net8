using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Domain.Common;
using WebBlog.Domain.Entities;
using WebBlog.Domain.Enums;

namespace WebBlog.Application.Dtos.Post
{
    public class PostDto : BaseDomainEntity
    {
        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;

        public string? Thumbnail { get; set; }

        public int? ViewCount { get; set; }

        public int UpvoteCount { get; set; }

        public int DowVoteCount { get; set; }

        public Guid? UserId { get; set; }

        public PostType PostType { get; set; }
    }
}
