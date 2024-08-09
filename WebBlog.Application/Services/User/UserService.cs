using AutoMapper;
using WebBog.Application.Dtos;
using WebBog.Application.Exceptions;
using WebBog.Application.Repositories;
using WebBog.Application.Services.CurrentUser;
using WebBog.Domain.Enums;
using WebBog.Domain.Payloads;
using WebBog.Domain.Specifications.Users;
using Microsoft.AspNetCore.Http;

namespace WebBog.Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserService(ICurrentUserService currentUserService, IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<PaginatedResult<UserDto>> GetPaginationUserAsync(int page, int pageSize)
        {
            var userDtos = await _userRepository.ToListAsync<UserDto>(orderBy: query => query.OrderBy(user => user.Id), page: page, size: pageSize);
            var userCount = await _userRepository.CountAsync();
            return new PaginatedResult<UserDto>(userCount, userDtos);
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
