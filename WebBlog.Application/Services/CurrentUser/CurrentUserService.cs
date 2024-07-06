using WebBog.Application.Exceptions;
using WebBog.Application.Repositories;
using WebBog.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace WebBog.Application.Services.CurrentUser
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
