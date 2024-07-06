using AutoMapper;
using WebBog.Application.Repositories;
using WebBog.Domain.Entities;
namespace WebBog.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
    }
}