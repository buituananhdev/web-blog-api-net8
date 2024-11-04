using WebBlog.Application.Exceptions;
using WebBlog.Application.Repositories;
using WebBlog.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace WebBlog.Application.Services.CurrentUser
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                var userIdClaim = (_httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value)
                    ?? throw new CustomException(StatusCodes.Status403Forbidden, ErrorCodeEnum.InvalidToken, "Invalid Token");
                return Guid.Parse(userIdClaim);
            }
        }
    }
}
