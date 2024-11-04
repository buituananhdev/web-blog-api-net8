using System.Linq.Expressions;
using WebBlog.Domain.Entities;

namespace WebBlog.Domain.Specifications.Users;

public class UserIdSpecification(Guid id) : Specification<User>
{
    public override Expression<Func<User, bool>> ToExpression()
    {
        return user => user.Id == id;
    }
}