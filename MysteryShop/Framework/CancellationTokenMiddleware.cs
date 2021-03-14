﻿using Microsoft.AspNetCore.Http;
using MysteryShop.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MysteryShop.Framework
{
    public class CancellationTokenMiddleware : IMiddleware
    {
        private readonly ICancellationTokenService _tokenManager;

        public CancellationTokenMiddleware(ICancellationTokenService tokenManager)
        {
            _tokenManager = tokenManager;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (await _tokenManager.IsCurrentActiveToken())
            {
                await next(context);

                return;
            }
            context.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
        }
    }
}

