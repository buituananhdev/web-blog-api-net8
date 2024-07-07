using System.Linq.Expressions;
using WebBlog.Domain.Entities;
using WebBog.Domain.Entities;
using WebBog.Domain.Specifications;

namespace WebBlog.Domain.Specifications.Comments
{
    public class CommentPostIdSpecification(Guid postId) : Specification<Comment>
    {
        public override Expression<Func<Comment, bool>> ToExpression()
        {
            return comment => comment.PostId == postId;
        }
    }
}