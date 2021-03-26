using MysteryShop.Domain.Entities;
using MysteryShop.Domain.Exceptions;
using MysteryShop.Domain.Repositories;
using MysteryShop.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MysteryShop.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokens;
        private readonly IJwtHandler _jwtHandler;
        private readonly IEncrypter _encrypter;

        public IdentityService(IJwtHandler jwtHandler,
            IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, IEncrypter encrypter)

        {
            _jwtHandler = jwtHandler;
            _userRepository = userRepository;
            _refreshTokens = refreshTokenRepository;
            _encrypter = encrypter;

        }

        public async Task Register(string login, string email, string name, string surname, string password)
        {
            var unemail = await _userRepository.IsEmailUnique(email);
            var unelogin = await _userRepository.IsLoginUnique(login);
            if (!unemail) throw new NewException(NewCodes.NotUniqueEmail);

            if (!unelogin) throw new NewException(NewCodes.NotUniqueLogin);

            var salt = _encrypter.GetSalt(password);
            var hash = _encrypter.GetHash(password, salt);

            User user = new User(email, name, surname, login, password, _encrypter, 1);

            await _userRepository.AddAsync(user);
        }

        public async Task<JwtDTO> Login(string login, string password)
        {
            var user = await _userRepository.GetAsync(login);
            if (user == null)
            {
                throw new NewException(NewCodes.UserNotFound);
            }
            var hash = _encrypter.GetHash(password, user.Salt);
            if (user.Password != hash)
            {
                throw new NewException(NewCodes.WrongCredentials);
            }
            var jwt = _jwtHandler.CreateToken(user.Id);
            var refreshToken = await _refreshTokens.GetByUserIdAsync(user.Id);
            string token = "";
            if (refreshToken == null)
            {
                token = Guid.NewGuid().ToString()
                .Replace("+", string.Empty)
                .Replace("=", string.Empty)
                .Replace("/", string.Empty);
                await _refreshTokens.AddAsync(new RefreshToken(user, token));
            }
            else
                token = refreshToken.Token;
            var jwtDto = new JwtDTO() { AccessToken = jwt.AccessToken, Expires = jwt.Expires, RefreshToken = token, UserId = jwt.UserId};

            return jwtDto;
        }

        public async Task<JwtDTO> RefreshAccessToken(string refreshToken)
        {
            var token = await _refreshTokens.GetAsync(refreshToken);
            if (token == null)
            {
                throw new NewException("Token was not found.");
            }
            if (token.Revoked)
            {
                throw new NewException("Token was revoked.");
            }
            var user = await _userRepository.GetAsync(token.UserId);
            if (user == null)
            {
                throw new NewException("User was not found.");
            }
            var jwt = _jwtHandler.CreateToken(user.Id);
            var jwtDto = new JwtDTO() { AccessToken = jwt.AccessToken, Expires = jwt.Expires, RefreshToken = token.Token };

            return jwtDto;
        }

        public async Task RevokeRefreshToken(string refreshToken)
        {
            var token = await _refreshTokens.GetAsync(refreshToken);
            if (token == null)
            {
                throw new Exception("Token not found.");
            }
            if (token.Revoked)
            {
                throw new Exception("Token was already revoked.");
            }
            token.Revoke();
            await _refreshTokens.UpdateAsync();
        }
    }
}
