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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IRatingRepository ratingRepository, IUserRepository userRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _ratingRepository = ratingRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(string title, string description, User userN)
        {
            if (title == null)
            {
                throw new Exception();
            }
            if (description == null)
            {
                throw new Exception();
            }

            var rating = _ratingRepository.CreateRating();
            var user = await _userRepository.GetAsync(userN.Id);

            if (user == null)
            {
                throw new Exception();
            }


            var newProduct = new Product(user, title, description, rating);

            await _productRepository.AddAsync(newProduct);
        }

        public async Task<ProductDTO> GetAsync(Guid id)
        {
            var product = await _productRepository.GetAsync(id);
            return _mapper.Map<Product, ProductDTO>(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync(int page, int count = 10)
        {
            var product = await _productRepository.GetAllAsync(page, count);
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllWithNameAsync(string title)
        {
            var product = await _productRepository.GetAllWithNameAsync(title);
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllUserProductsAsync(User userN)
        {
            var product = await _productRepository.GetAllUserProductsAsync(userN);
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(product);
        }

        public async Task RemoveAsync(Guid id)
        {
            await _productRepository.RemoveAsync(id);
        }
    }
}
