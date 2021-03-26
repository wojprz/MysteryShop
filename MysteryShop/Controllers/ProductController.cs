using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MysteryShop.Domain.Entities;
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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [AllowAnonymous]
        [HttpPost("CreateProduct")]
        public async Task CreateProduct([FromBody] ProductModel product)
        {
           await _productService.CreateAsync(product.Title, product.Description, product.UserID);
        }

        [HttpGet("GetById")]
        public async Task<ProductDTO> GetById(Guid id)
        {
            return await _productService.GetAsync(id);
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IEnumerable<ProductDTO>> GetAllWithPagination(int page, int count)
        {
            return await _productService.GetAllAsync(page, count);
        }

        [AllowAnonymous]
        [HttpGet("GetSearch")]
        public async Task<IEnumerable<ProductDTO>> GetSearch(string title)
        {
            return await _productService.GetAllWithNameAsync(title);
        }

        [AllowAnonymous]
        [HttpGet("GetAllUserProducts")]
        public async Task<IEnumerable<ProductDTO>> GetAllUserProducts(Guid userID)
        {
            return await _productService.GetAllUserProductsAsync(userID);
        }
        
        [HttpDelete("Remove")]
        public async Task<IActionResult> RemoveProduct(Guid id)
        {
            await _productService.RemoveAsync(id);
            return Ok();
        }
    }
}
