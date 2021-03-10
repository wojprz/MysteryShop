using Microsoft.EntityFrameworkCore;
using MysteryShop.Domain.Contexts;
using MysteryShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MysteryShop.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MysteryShopContext _entities;

        public UserRepository(MysteryShopContext entities)
        {
            _entities = entities;
        }

        public async Task AddAsync(User user)
        {
            await _entities.Users.AddAsync(user);
            await _entities.SaveChangesAsync();
        }

        public async Task<User> GetAsync(Guid id) => await _entities.Users.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<User> GetAsync(string login) => await _entities.Users.SingleOrDefaultAsync(x => x.Login == login);

        public async Task<IEnumerable<User>> GetAllAsync() => await _entities.Users.ToListAsync();

        public async Task<User> GetUserWithComments(Guid id) => await _entities.Users.Include("Comment").SingleOrDefaultAsync(x => x.Id == id);

        public async Task<User> GetUserWithComments(string login) => await _entities.Users.Include("Comment").SingleOrDefaultAsync(x => x.Login == login);

        public async Task RemoveAsync(Guid id)
        {
            var user = await GetAsync(id);
            _entities.Users.Remove(user);
            await _entities.SaveChangesAsync();
        }

        public async Task RemoveAsync(string login)
        {
            var user = await GetAsync(login);
            _entities.Users.Remove(user);
            await _entities.SaveChangesAsync();
        }

        public async Task<bool> IsEmailUnique(string email)
        {
            var z = await _entities.Users.AnyAsync(x => x.Email == email);
            return !z;
        }

        public async Task<bool> IsLoginUnique(string login)
        {
            var z = await _entities.Users.AnyAsync(x => x.Login == login);
            return !z;
        }

        public async Task UpdateAsync(User user)
        {
            _entities.Users.Update(user);
            await _entities.SaveChangesAsync();
        }
        public async Task UpdateStatusAsync(Guid id, int status)
        {
            var user = await GetAsync(id);
            user.SetStatus(status);
            await _entities.SaveChangesAsync();
        }
    }
}
