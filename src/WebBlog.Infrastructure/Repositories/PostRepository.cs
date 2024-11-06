using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Application.Repositories;
using WebBlog.Domain.Entities;

namespace WebBlog.Infrastructure.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
