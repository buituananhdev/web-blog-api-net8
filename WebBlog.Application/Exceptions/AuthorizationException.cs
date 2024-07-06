using WebBog.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace WebBog.Application.Exceptions
{
    public class AuthorizationException : CustomException
    {
        public AuthorizationException(string message) : base(StatusCodes.Status403Forbidden, ErrorCodeEnum.NotAuthorized, message)
        {
        }
    }
}
