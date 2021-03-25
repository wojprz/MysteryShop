using MysteryShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MysteryShop.Infrastructure.Services
{
    public interface IUserService
    {
        Task ChangeEmail(string login, string email);
        Task ChangePassword(string login, string newPassword, string oldPassword);
        Task<User> GetUser(Guid id);
        Task RemoveUser(Guid id);
    }
}
