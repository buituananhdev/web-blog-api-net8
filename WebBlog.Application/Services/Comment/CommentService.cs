using AutoMapper;
using Microsoft.Extensions.Logging;
using WebBlog.Application.Dtos;
using WebBlog.Domain.Entities;
using WebBlog.Domain.Specifications.Comments;
using WebBog.Application.Repositories;
using WebBog.Application.Services.CurrentUser;
using WebBog.Domain.Payloads;

namespace WebBlog.Service.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CommentService> _logger;
        private readonly ICommentRepository _commentRepository;
        private readonly ICurrentUserService _currentUserService;

        public CommentService(IUnitOfWork unitOfWork, ICommentRepository commentRepository, ICurrentUserService currentUserService, IMapper mapper, ILogger<CommentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _commentRepository = commentRepository;
            _currentUserService = currentUserService;
        }

        public async Task<CommentDto> AddComment(CommentDto comment)
        {
            try
            {
                var commentEntity = _mapper.Map<Comment>(comment);
                commentEntity.UserId = _currentUserService.UserId;
                await _commentRepository.AddAsync(commentEntity);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<CommentDto>(commentEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Add comment error");
                throw;
            }
        }

        public async Task<CommentDto> DeleteComment(Guid commentID)
        {
            try
            {
                var commentEntity = await _commentRepository.GetByIdAsync(commentID);
                if (commentEntity == null)
                {
                    return null;
                }
                _commentRepository.Delete(commentEntity);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<CommentDto>(commentEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete comment error");
                throw;
            }
        }

        public async Task<PaginatedResult<CommentDto>> GetCommentsForPost(Guid postId, int page, int pageSize)
        {
            try
            {
                var commentDtos = await _commentRepository.ToListAsync<CommentDto>(spec: new CommentPostIdSpecification(postId), orderBy: q => q.OrderBy(c => c.CreatedAt), page: page, size: pageSize);
                var commentCount = await _commentRepository.CountAsync();
                return new PaginatedResult<CommentDto>(commentCount, commentDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get comments error");
                throw;
            }
        }
    }
}
