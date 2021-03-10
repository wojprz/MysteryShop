using MysteryShop.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MysteryShop.Infrastructure.Services
{
    public interface IJwtHandler
    {
        JwtDTO CreateToken(Guid userId);
    }
}
