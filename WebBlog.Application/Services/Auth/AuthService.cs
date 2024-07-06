using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using WebBog.Application.Utils;
using WebBog.Application.Dtos;
using WebBog.Domain.Payloads;
using WebBog.Application.Exceptions;
using WebBog.Application.ExternalServices;
using WebBog.Application.Repositories;
using WebBog.Domain.Enums;
using WebBog.Domain.Specifications.Users;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebBog.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper, IEmailService emailService, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mapper = mapper;
            _emailService = emailService;
            _userRepository = userRepository;
        }


        public async Task<TokenPayload> LoginAsync(TokenObtainPairDto tokenObtainPair)
        {
            var user = await _userRepository.FirstOrDefaultAsync(new UserEmailSpecification(tokenObtainPair.Email!));
            if (user == null || !BCrypt.Net.BCrypt.Verify(tokenObtainPair.Password, user.Password))
            {
                throw new CustomException(StatusCodes.Status401Unauthorized, ErrorCodeEnum.IncorrectEmailOrPassword, "Incorrect email or password.");
            }

            var tokenPayload = JwtUtil.GenerateAccessToken(user, _configuration);
            return tokenPayload;
        }
    }
}
