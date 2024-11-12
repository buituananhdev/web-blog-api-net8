using AutoMapper;
using System.Collections.Generic;
using WebBlog.Application.Dtos.Post;
using WebBlog.Application.Exceptions;
using WebBlog.Application.Repositories;
using WebBlog.Application.Services.Cache;
using WebBlog.Application.Services.CurrentUser;
using WebBlog.Domain.Entities;
using WebBlog.Domain.Payloads;

namespace WebBlog.Application.Services.Post
{
    internal class PostService : IPostService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private readonly ICacheService _cacheService;
        public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService, ICacheService cacheService, IMapper mapper)
        {
            _cacheService = cacheService;
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        public async Task<PostDto> CreatePostAsync(CreatePostDto postDto)
        {
            var post = _mapper.Map<Domain.Entities.Post>(postDto);
            post.UserId = _currentUserService.UserId;

            await _postRepository.AddAsync(post);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<PostDto>(post);
        }

        public Task<PostDto?> DeletePostAsync(Guid postID)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatedResult<PostDto>> GetPaginationGetPopularPostsAsync(int page, int pageSize)
        {
            string cacheKey = $"PaginationPopularPosts_Page{page}_PageSize{pageSize}";
            var cachedResult = await _cacheService.GetCacheAsync<PaginatedResult<PostDto>>(cacheKey);
            if (cachedResult != null)
            {
                return cachedResult;
            }

            var postsTask = _postRepository.ToListAsync<PostDto>(
                orderBy: query => query.OrderByDescending(post => post.Votes.Sum(v => (int)v.VoteType)).ThenByDescending(post => post.ViewCount),
                page: page,
                size: pageSize
            );

            var postCountTask = _postRepository.CountAsync();

            await Task.WhenAll(postsTask, postCountTask);

            var postDtos = postsTask.Result;
            var postCount = postCountTask.Result;

            var result = new PaginatedResult<PostDto>(postCount, postDtos);
            await _cacheService.SetCacheAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }

        public async Task<PostDto> GetPostAsync(Guid postID)
        {
            var post = await _postRepository.GetByIdAsync(postID);
            if (post is null)
            {
                throw new NotFoundException("Post not found!");
            }
            post.ViewCount++;
            return _mapper.Map<PostDto>(post);
        }

        public Task<PostDto> UpdatePostAsync(Guid postID, PostDto post)
        {
            throw new NotImplementedException();
        }
    }
}
