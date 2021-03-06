using Microsoft.EntityFrameworkCore;
using MysteryShop.Domain.Contexts;
using MysteryShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryShop.Domain.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MysteryShopContext _entities;

        public ProductRepository(MysteryShopContext entities)
        {
            _entities = entities;
        }

        public async Task AddAsync(Product product)
        {
            await _entities.Products.AddAsync(product);
            await _entities.SaveChangesAsync();
        }

        public async Task<Product> GetAsync(Guid id) => await _entities.Products.Include(x => x.User).Include(x => x.Rating).SingleOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Product>> GetAllAsync() => await _entities.Products.ToListAsync();

        public async Task<IEnumerable<Product>> GetAllAsync(int page, int count) => await _entities.Products.Include(x => x.Rating).Include(x => x.User).Skip((page - 1) * count).Take(count).OrderBy(x => x.DateOfAddition).ToListAsync();

        public async Task<IEnumerable<Product>> GetAllWithNameAsync(string title, int page, int count) => await _entities.Products.Include(x => x.User).Include(x => x.Rating).Where(x => x.Title.Contains(title)).Skip((page - 1) * count).Take(count).OrderBy(x => x.DateOfAddition).ToListAsync();

        public async Task<IEnumerable<Product>> GetAllUserProductsAsync(User user, int page, int count) => await _entities.Products.Include(x => x.User).Include(x => x.Rating).Where(x => x.User == user).ToListAsync();

        public async Task RemoveAsync(Guid id)
        {
            var product = await GetAsync(id);
            _entities.Products.Remove(product);
            await _entities.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _entities.Products.Update(product);
            await _entities.SaveChangesAsync();
        }

        public async Task UpdateStatusAsync(Guid id, int status)
        {
            var product = await GetAsync(id);
            product.SetStatus(status);
            await _entities.SaveChangesAsync();
        }
    }
}
