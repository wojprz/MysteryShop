using MysteryShop.Domain.Entities;
using MysteryShop.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MysteryShop.Infrastructure.Services
{
    public interface IRatingService
    {
        Task AddVoteAsync(Product product, User user, int rating);
        Task<RatingDTO> GetRatingAsync(Guid productId);
    }
}
