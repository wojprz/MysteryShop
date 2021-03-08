using MysteryShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MysteryShop.Domain.Repositories
{
    public interface IRatingRepository
    {
        Task AddAsync(Rating rating);
        Rating CreateRating();
        Task<IEnumerable<Rating>> GetAllAsync();
        Task AddVoteAsync(Product product, User user, int rating);
        Task<Rating> GetRatingAsync(Guid productId);
    }
}
