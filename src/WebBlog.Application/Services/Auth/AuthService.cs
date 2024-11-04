using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using WebBlog.Application.Utils;
using WebBlog.Application.Dtos;
using WebBlog.Domain.Payloads;
using WebBlog.Application.Exceptions;
using WebBlog.Application.ExternalServices;
using WebBlog.Application.Repositories;
using WebBlog.Domain.Enums;
using WebBlog.Domain.Specifications.Users;
using Microsoft.AspNetCore.Http.HttpResults;
using WebBlog.Application.Services.User;

namespace WebBlog.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper, IEmailService emailService, IUserRepository userRepository, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mapper = mapper;
            _emailService = emailService;
            _userRepository = userRepository;
            _userService = userService;
        }


        public async Task<TokenPayload> LoginAsync(LoginDto tokenObtainPair)
        {
            var user = await _userRepository.FirstOrDefaultAsync(new UserEmailSpecification(tokenObtainPair.Email!));
            if (user == null || !BCrypt.Net.BCrypt.Verify(tokenObtainPair.Password, user.Password))
            {
                throw new CustomException(StatusCodes.Status401Unauthorized, ErrorCodeEnum.IncorrectEmailOrPassword, "Incorrect email or password.");
            }

            var tokenPayload = JwtUtil.GenerateAccessToken(user, _configuration);
            return tokenPayload;
        }

        public async Task<UserDto> RegisterAsync(RegistrationDto registrationDto)
        {
            var isUserExist = await _userService.IsEmailAlreadyExist(registrationDto.Email);
            if (isUserExist)
            {
                throw new EmailExistedException();
            }

            registrationDto.Password = BCrypt.Net.BCrypt.HashPassword(registrationDto.Password);
            
            var user = await _userService.AddUserAsync(_mapper.Map<UserDto>(registrationDto));
            return user;
        }
    }
}
