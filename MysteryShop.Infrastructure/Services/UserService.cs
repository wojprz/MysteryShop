using MysteryShop.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MysteryShop.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;

        public UserService(IUserRepository userRepository, IEncrypter encrypter)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
        }

        public async Task ChangeEmail(string login, string email)
        {
            var user = await _userRepository.GetAsync(login);
            var unemail = await _userRepository.IsEmailUnique(email);

            if (user.Email == email) throw new Exception();
            if (!unemail) throw new Exception();

            user.SetEmail(email);

            await _userRepository.UpdateAsync(user);
        }

        public async Task ChangePassword(string login, string newPassword, string oldPassword)
        {
            var user = await _userRepository.GetAsync(login);

            user.SetPassword(newPassword, _encrypter);

            await _userRepository.UpdateAsync(user);
        }
    }
}
