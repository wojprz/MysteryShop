using MysteryShop.Domain.Entities;
using MysteryShop.Domain.Exceptions;
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

        public async Task<User> GetUserAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null)
                throw new NewException(NewCodes.UserNotFound);
            return user;
        }

        public async Task RemoveUser(Guid id)
        {
            await _userRepository.RemoveAsync(id);
        }

        public async Task ChangeEmail(string login, string email)
        {
            var user = await _userRepository.GetAsync(login);
            if (user == null)
                throw new NewException(NewCodes.UserNotFound);
            var unemail = await _userRepository.IsEmailUnique(email);

            if (user.Email == email) throw new NewException(NewCodes.SameEmail);
            if (!unemail) throw new NewException(NewCodes.NotUniqueEmail);

            user.SetEmail(email);

            await _userRepository.UpdateAsync(user);
        }

        public async Task ChangePassword(string login, string newPassword, string oldPassword)
        {
            var user = await _userRepository.GetAsync(login);
            if (user == null)
                throw new NewException(NewCodes.UserNotFound);
            user.SetPassword(newPassword, _encrypter);

            await _userRepository.UpdateAsync(user);
        }
    }
}
