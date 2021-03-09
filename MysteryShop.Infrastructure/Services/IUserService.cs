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
    }
}
