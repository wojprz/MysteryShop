using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MysteryShop.Infrastructure.Models;
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
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratignService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public RatingController(IRatingService ratingService, IUserService userService, IProductService productService)
        {
            _ratignService = ratingService;
            _userService = userService;
            _productService = productService;
        }

        [AllowAnonymous]
        [HttpPost("Vote")]
        public async Task<IActionResult> Vote ([FromBody]RatingModel rating)
        {
            var user = await _userService.GetUserAsync(rating.UserID);
            var product = await _productService.Get(rating.ProductID);
            await _ratignService.AddVoteAsync(product, user, rating.Rate);
            return Ok();
        }
    }
}
