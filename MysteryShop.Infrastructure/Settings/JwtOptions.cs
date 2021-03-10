using System;
using System.Collections.Generic;
using System.Text;

namespace MysteryShop.Infrastructure.Settings
{
    public class JwtOptions
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public int ExpiryMinutes { get; set; }

    }
}
