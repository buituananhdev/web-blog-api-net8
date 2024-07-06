using WebBog.Application.Dtos;
using WebBog.Domain.Payloads;

namespace WebBog.Application.Services.Auth;


public interface IAuthService
{
    Task<TokenPayload> LoginAsync(TokenObtainPairDto loginDto);
}