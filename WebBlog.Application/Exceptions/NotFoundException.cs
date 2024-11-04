
using WebBlog.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace WebBlog.Application.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message) : base(StatusCodes.Status404NotFound, ErrorCodeEnum.NotFound, message)
        {
        }
    }
}
