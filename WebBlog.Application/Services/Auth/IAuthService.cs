using WebBog.Application.Dtos;
using WebBog.Domain.Payloads;

namespace WebBog.Application.Services.Auth;


public interface IAuthService
{
    Task<TokenPayload> LoginAsync(LoginDto loginDto);
    Task<UserDto> RegisterAsync(RegistrationDto registrationDto);
}