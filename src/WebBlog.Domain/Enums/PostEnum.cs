using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBlog.Domain.Enums
{
    public enum PostType
    {
        Blog = 0,
        Question = 1
    }

    public enum VoteType
    {
        Up = 1,
        Down = -1
    }
}
