using MysteryShop.Domain.Entities;
using MysteryShop.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MysteryShop.Infrastructure.Services
{
    public interface IProductService
    {
        Task CreateAsync(string title, string description, Guid userID, double price);
        Task<ProductDTO> GetAsync(Guid id);
        Task<IEnumerable<ProductDTO>> GetAllAsync(int page, int count = 10);
        Task<IEnumerable<ProductDTO>> GetAllWithNameAsync(string title, int page, int count = 10);
        Task<IEnumerable<ProductDTO>> GetAllUserProductsAsync(Guid userID, int page, int count = 10);
        Task RemoveAsync(Guid id);
        Task<Product> Get(Guid id);
    }
}
