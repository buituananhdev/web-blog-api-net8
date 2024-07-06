using System.Linq.Expressions;
using WebBog.Domain.Entities;

namespace WebBog.Domain.Specifications.Users;

public class UserIdSpecification(Guid id) : Specification<User>
{
    public override Expression<Func<User, bool>> ToExpression()
    {
        return user => user.Id == id;
    }
}