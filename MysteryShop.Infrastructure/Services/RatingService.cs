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
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IMapper _mapper;

        public RatingService(IProductRepository productRepository, IRatingRepository ratingRepository, IMapper mapper)
        {
            _ratingRepository = ratingRepository;
            _mapper = mapper;
        }

        public async Task AddVoteAsync(Product product, User user, int rating)
        {
            await _ratingRepository.AddVoteAsync(product, user, rating);
        }

        public async Task<RatingDTO> GetRatingAsync(Guid productId)
        {
            var rating = await _ratingRepository.GetRatingAsync(productId);
            return _mapper.Map<Rating, RatingDTO>(rating);
        }
    }
}
