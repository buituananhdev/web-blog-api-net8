using Microsoft.AspNetCore.Mvc;
using WebBlog.Api.Params;
using WebBlog.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using WebBlog.Application.Services.User;

namespace WebBlog.Api.Controllers
{
    /// <summary>
    /// Controller for managing user-related operations.
    /// </summary>
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get a paginated list of users.
        /// </summary>
        /// <param name="page_size">The number of users per page.</param>
        /// <param name="page">The page number.</param>
        /// <returns>Returns a paginated list of users.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetUsers(PaginationParams paginationParams)
        {
            var users = await _userService.GetPaginationUserAsync(paginationParams.Page, paginationParams.PageSize);
            return Ok(users);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _userService.GetCurrentUserAsync();
            return Ok(user);
        }
    }
}
