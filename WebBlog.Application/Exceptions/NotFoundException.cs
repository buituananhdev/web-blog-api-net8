
using WebBog.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace WebBog.Application.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message) : base(StatusCodes.Status404NotFound, ErrorCodeEnum.NotFound, message)
        {
        }
    }
}
