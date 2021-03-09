﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MysteryShop.Infrastructure.Services
{
    public interface ICancellationTokenService
    {
        Task<bool> IsCurrentActiveToken();
        Task DeactivateCurrentAsync();
        Task<bool> IsActiveAsync(string accessToken);
        Task DeactivateAsync(string accessToken);
    }
}
