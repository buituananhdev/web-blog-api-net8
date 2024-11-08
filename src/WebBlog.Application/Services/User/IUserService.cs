using System.Globalization;
using WebBlog.Application.Dtos;
using WebBlog.Domain.Payloads;

namespace WebBlog.Application.Services.User;

public interface IUserService
{
    Task<UserDto?> GetMe();
    Task<PaginatedResult<UserDto>> GetPaginationUserAsync(int page, int pageSize);
    Task<UserDto> UpdateUserByIdAsync(UpdateUserDto adminUpdateUserDto);
    Task<bool> IsEmailAlreadyExist(string email);
    Task<UserDto> AddUserAsync(UserDto userDto);
    Task<UserDto> GetCurrentUserAsync();
}