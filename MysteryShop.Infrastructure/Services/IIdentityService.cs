using MysteryShop.Infrastructure.DTOs;
using System.Threading.Tasks;

namespace MysteryShop.Infrastructure.Services
{
    public interface IIdentityService
    {
        Task<JwtDTO> Login(string login, string password);
        Task Register(string login, string email, string name, string surname, string password);
        Task<JwtDTO> RefreshAccessToken(string refreshToken);
        Task RevokeRefreshToken(string refreshToken);
    }
}