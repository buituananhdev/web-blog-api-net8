using WebBlog.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace WebBlog.Application.Exceptions
{
    public class EmailExistedException: CustomException
    {
        public EmailExistedException() : base(StatusCodes.Status409Conflict, ErrorCodeEnum.ExistedEmail, "Email already exists")
        {
        }
    }
}
