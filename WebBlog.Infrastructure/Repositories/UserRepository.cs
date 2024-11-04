using AutoMapper;
using WebBlog.Application.Repositories;
using WebBlog.Domain.Entities;
namespace WebBlog.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
    }
}