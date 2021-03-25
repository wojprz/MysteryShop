using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MysteryShop.Domain.Entities;
using MysteryShop.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MysteryShop.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _userService.GetUser(id);
            return Ok(user);
        }

        [HttpDelete("RemoveUser")]
        public async Task<IActionResult> RemoveUser(Guid id)
        {
            await _userService.RemoveUser(id);
            return Ok();
        }

        [HttpPut("ChangeEmail")]
        public async Task<IActionResult> ChangeEmail(Guid id, string newEmail)
        {
            var user = await _userService.GetUser(id);
            await _userService.ChangeEmail(user.Login, newEmail);
            return Ok();
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword(Guid id, string oldPassword, string newPassword)
        {
            var user = await _userService.GetUser(id);
            await _userService.ChangePassword(user.Login, newPassword, oldPassword);
            return Ok();
        }

    }
}
