using AutoMapper;
using WebBlog.Application.Dtos;
using WebBlog.Application.Exceptions;
using WebBlog.Application.Repositories;
using WebBlog.Application.Services.CurrentUser;
using WebBlog.Domain.Enums;
using WebBlog.Domain.Payloads;
using WebBlog.Domain.Specifications.Users;
using Microsoft.AspNetCore.Http;
using WebBlog.Application.Services.Cache;

namespace WebBlog.Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ICacheService _cacheService;
        public UserService(ICurrentUserService currentUserService, IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository, ICacheService cacheService)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
            _cacheService = cacheService;
        }

        public async Task<PaginatedResult<UserDto>> GetPaginationUserAsync(int page, int pageSize)
        {
            string cacheKey = $"PaginationUser_Page{page}_PageSize{pageSize}";
            var cachedResult = await _cacheService.GetCacheAsync<PaginatedResult<UserDto>>(cacheKey);
            if (cachedResult != null)
            {
                return cachedResult;
            }

            var userDtos = await _userRepository.ToListAsync<UserDto>(orderBy: query => query.OrderBy(user => user.Id), page: page, size: pageSize);
            var userCount = await _userRepository.CountAsync();

            var result = new PaginatedResult<UserDto>(userCount, userDtos);
            await _cacheService.SetCacheAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }

        public async Task<UserDto?> GetMe()
        {
            return await _userRepository.FirstOrDefaultAsync<UserDto>(new UserIdSpecification(_currentUserService.UserId));
        }

        public async Task<UserDto> UpdateUserByIdAsync(UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetByIdAsync(_currentUserService.UserId)
                       ?? throw new UserIdNotFoundException();
            var otherUser = await _userRepository.FirstOrDefaultAsync(new UserEmailSpecification(updateUserDto.Email!));
            if (otherUser != null && otherUser.Id != user.Id)
            {
                throw new EmailExistedException();
            }
            _mapper.Map(updateUserDto, user);
            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
            var userDto = await _userRepository.FirstOrDefaultAsync<UserDto>(new UserIdSpecification(user.Id));
            return userDto!;
        }

        public Task<bool> IsEmailAlreadyExist(string email)
        {
            return _userRepository.AnyAsync(new UserEmailSpecification(email));
        }

        public async Task<UserDto> AddUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<Domain.Entities.User>(userDto);

            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetCurrentUserAsync()
        {
            return await _userRepository.FirstOrDefaultAsync<UserDto>(new UserIdSpecification(_currentUserService.UserId));
        }
    }
}
