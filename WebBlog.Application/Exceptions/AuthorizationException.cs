using WebBlog.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace WebBlog.Application.Exceptions
{
    public class AuthorizationException : CustomException
    {
        public AuthorizationException(string message) : base(StatusCodes.Status403Forbidden, ErrorCodeEnum.NotAuthorized, message)
        {
        }
    }
}
