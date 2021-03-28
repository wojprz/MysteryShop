using AutoMapper;
using MysteryShop.Domain.Entities;
using MysteryShop.Domain.Exceptions;
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

        public async Task CreateAsync(string title, string description, Guid userID, double price)
        {
            if (title == null)
            {
                throw new NewException(NewCodes.EmptyTitle);
            }
            if (description == null)
            {
                throw new NewException(NewCodes.EmptyDescryption);
            }

            var rating = _ratingRepository.CreateRating();
            var user = await _userRepository.GetAsync(userID);

            if (user == null)
            {
                throw new NewException(NewCodes.UserNotFound);
            }


            var newProduct = new Product(user, title, description, rating, price);

            await _productRepository.AddAsync(newProduct);
        }
        public async Task<Product> Get(Guid id)
        {
            var product = await _productRepository.GetAsync(id);
            if (product == null)
                throw new NewException(NewCodes.ProductNotFound);
            return product;
        }

        public async Task<ProductDTO> GetAsync(Guid id)
        {
            var product = await _productRepository.GetAsync(id);
            if (product == null)
                throw new NewException(NewCodes.ProductNotFound);
            return _mapper.Map<Product, ProductDTO>(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync(int page, int count = 10)
        {
            var product = await _productRepository.GetAllAsync(page, count);
            if (product == null)
                throw new NewException(NewCodes.ProductNotFound);
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllWithNameAsync(string title, int page, int count = 10)
        {
            var product = await _productRepository.GetAllWithNameAsync(title, page, count);
            if (product == null)
                throw new NewException(NewCodes.ProductNotFound);
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllUserProductsAsync(Guid userID, int page, int count = 10)
        {
            var user = await _userRepository.GetAsync(userID);
            if (user == null)
                throw new NewException(NewCodes.UserNotFound);
            var product = await _productRepository.GetAllUserProductsAsync(user, page, count);
            if (product == null)
                throw new NewException(NewCodes.ProductNotFound);
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(product);
        }

        public async Task RemoveAsync(Guid id)
        {
            var product = await _productRepository.GetAsync(id);
            if (product == null)
                throw new NewException(NewCodes.ProductNotFound);
            await _productRepository.RemoveAsync(id);
        }
    }
}
