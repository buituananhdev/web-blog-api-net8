using WebBlog.Application.Dtos;
using WebBlog.Domain.Payloads;

namespace WebBlog.Application.Services.Auth;


public interface IAuthService
{
    Task<TokenPayload> LoginAsync(LoginDto loginDto);
    Task<UserDto> RegisterAsync(RegistrationDto registrationDto);
}