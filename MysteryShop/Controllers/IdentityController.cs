using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MysteryShop.Infrastructure.DTOs;
using MysteryShop.Infrastructure.Models;
using MysteryShop.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace MysteryShop.Controllers
{

    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly ICancellationTokenService _cancellationService;

        public IdentityController(IIdentityService identityService, ICancellationTokenService cancellationTokenService)
        {
            _identityService = identityService;
            _cancellationService = cancellationTokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var user = await _identityService.Login(login.Login, login.Password);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<JwtDTO> RefreshToken(string token)
        {
            return await _identityService.RefreshAccessToken(token);
        }

        [HttpPost("revoke")]
        public async Task RevokeToken(string token)
        {
            await _identityService.RevokeRefreshToken(token);
        }

        [HttpPost("cancel")]
        public async Task CancelToken()
        {
            await _cancellationService.DeactivateCurrentAsync();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task Register([FromBody] RegisterModel register)
        {
            await _identityService.Register(register.Login, register.Email, register.Name, register.Surname, register.Password);
        }
    }
}
