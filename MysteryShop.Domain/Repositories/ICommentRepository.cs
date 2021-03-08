using MysteryShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MysteryShop.Domain.Repositories
{
    public interface ICommentRepository
    {
        Task AddAsync(Comment comment);
        Task<Comment> GetAsync(Guid id);
        Task<IEnumerable<Comment>> GetAllProductComments(Guid productId, int page, int count);
        Task<IEnumerable<Comment>> GetAllUserComments(Guid userId, int page, int count);
        Task<IEnumerable<Comment>> GetAllUserComments(string login, int page, int count);
        Task RemoveAsync(Guid commentId);
        Task UpdateAsync(Comment comment);
    }
}
