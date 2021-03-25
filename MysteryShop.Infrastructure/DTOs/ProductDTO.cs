using MysteryShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MysteryShop.Infrastructure.DTOs
{
    public class ProductDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public DateTime DateOfAddition { get; set; }
        public Guid UserID { get; set; }
        public RatingDTO Rating { get; set; }
    }
}
