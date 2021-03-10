using MysteryShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MysteryShop.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetAllAsync(int page, int count);
        Task<IEnumerable<Product>> GetAllWithNameAsync(string title);
        Task<IEnumerable<Product>> GetAllUserProductsAsync(User user);
        Task RemoveAsync(Guid id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task UpdateStatusAsync(Guid id, int status);
    }
}
