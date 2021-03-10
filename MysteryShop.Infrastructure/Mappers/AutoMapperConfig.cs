using AutoMapper;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MysteryShop.Domain.Entities;
using MysteryShop.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MysteryShop.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Mapper()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDTO>();
                cfg.CreateMap<Comment, CommentDTO>();
                cfg.CreateMap<Rating, RatingDTO>();
                cfg.CreateMap<User, UserDTO>();
            }).CreateMapper();
    }
}
