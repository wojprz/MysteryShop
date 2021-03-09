using MysteryShop.Domain.Entities;
using MysteryShop.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MysteryShop.Infrastructure.Services
{
    public interface ICommentService
    {

        Task<IEnumerable<CommentDTO>> GetAllUserComments(Guid userId, int page, int count);
        Task<IEnumerable<CommentDTO>> GetAllUserComments(string login, int page, int count);
        Task<IEnumerable<CommentDTO>> GetAllProdctComments(Guid productId, int page, int count);
        Task RemoveAsync(Guid commentId);
        Task<CommentDTO> GetAsync(Guid commentId);
        Task AddCommentAsync(Comment comment);
    }
}
