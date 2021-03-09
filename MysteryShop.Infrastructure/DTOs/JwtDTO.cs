using System;
using System.Collections.Generic;
using System.Text;

namespace MysteryShop.Infrastructure.DTOs
{
    public class JwtDTO
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public long Expires { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
