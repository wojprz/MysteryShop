using AutoMapper;
using MysteryShop.Domain.Entities;
using MysteryShop.Domain.Repositories;
using MysteryShop.Infrastructure.DTOs;
using MysteryShop.Infrastructure.Mappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MysteryShop.Infrastructure.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUserRepository _userRepoistory;
        private readonly IProductRepository _productRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(IProductRepository productRepository, IRatingRepository ratingRepository, IUserRepository userRepository, ICommentRepository commentRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _userRepoistory = userRepository;
            _ratingRepository = ratingRepository;
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CommentDTO>> GetAllUserComments(Guid userId, int page, int count)
        {
            var user = await _userRepoistory.GetAsync(userId);
            if (user == null) throw new Exception();
            var comments = await _commentRepository.GetAllUserComments(userId, page, count);
            return _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(comments);
        }

        public async Task<IEnumerable<CommentDTO>> GetAllUserComments(string login, int page, int count)
        {
            var user = await _userRepoistory.GetAsync(login);
            if (user == null) throw new Exception();
            var comments = await _commentRepository.GetAllUserComments(login, page, count);
            return _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(comments);
        }

        public async Task<IEnumerable<CommentDTO>> GetAllProdctComments(Guid productId, int page, int count)
        {
            var comments = await _commentRepository.GetAllProductComments(productId, page, count);
            return _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(comments);
        }

        public async Task RemoveAsync(Guid commentId)
        {
            await _commentRepository.RemoveAsync(commentId);
        }

        public async Task<CommentDTO> GetAsync(Guid commentId)
        {
            var comment = await _commentRepository.GetAsync(commentId);
            return _mapper.Map<Comment, CommentDTO>(comment);
        }

        public async Task AddCommentAsync(Guid userID, string content, Guid productID)
        {
            var user = await _userRepoistory.GetAsync(userID);
            var product = await _productRepository.GetAsync(productID);
            var comment = new Comment(content, user, product);
            await _commentRepository.AddAsync(comment);
        }
    }
}
