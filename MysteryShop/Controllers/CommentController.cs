using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MysteryShop.Infrastructure.DTOs;
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
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        
        [AllowAnonymous]
        [HttpGet("GetAllUserComments")]
        public async Task<IEnumerable<CommentDTO>> GetAllUserComments(Guid userID, int page, int count)
        {
            return await _commentService.GetAllUserComments(userID, page, count);
        }

        [AllowAnonymous]
        [HttpGet("GetAllProductComments")]
        public async Task<IEnumerable<CommentDTO>> GetAllProductComments(Guid productID, int page, int count)
        {
            return await _commentService.GetAllProdctComments(productID, page, count);
        }

        [AllowAnonymous]
        [HttpDelete("Remove")]
        public async Task<IActionResult> Remove(Guid commentID)
        {
            await _commentService.RemoveAsync(commentID);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("AddComment")]
        public async Task<IActionResult> AddComment([FromBody] CommentModel comment)
        {
            await _commentService.AddCommentAsync(comment.UserID, comment.Content, comment.ProductID);
            return Ok();
        }
    }
}
