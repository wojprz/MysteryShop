using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MysteryShop.Domain.Contexts;
using MysteryShop.Domain.Repositories;
using MysteryShop.Infrastructure.Mappers;
using MysteryShop.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using MysteryShop.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using MysteryShop.Framework;
using MysteryShop.Filters;

namespace MysteryShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            WebRootPath = env.WebRootPath;
        }

        public IConfiguration Configuration { get; }
        protected string WebRootPath { get; set; }



        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Serwer.Api", Version = "v1" });
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
            });

            var hostEnviroment = new HostEnviroment { RootPath = WebRootPath };
            var jwtSettings = new JwtOptions()
            {
                Issuer = "MysteryShopInc",
                ExpiryMinutes = 240,
                Key = "Fjjji0Hdsa4$JgrwIO1j678dCelgFymdo"
            };

            services.AddDbContext<MysteryShopContext>(
                options => options.UseInMemoryDatabase("MysteryShop"));

            services.AddDbContext<RefreshTokenContext>(
                options => options.UseInMemoryDatabase("RefreshToken"));

            //services.AddEntityFrameworkSqlServer()
            //   .AddEntityFrameworkInMemoryDatabase()
            //   .AddDbContext<MysteryShopContext>()
            //   .AddDbContext<RefreshTokenContext>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(c =>
                {
                    c.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                        ValidIssuer = jwtSettings.Issuer,
                        ValidateAudience = false,
                        ValidateLifetime = true
                    };
                });
           
            services.AddSingleton<IHostEnviroment>(hostEnviroment);
            services.AddSingleton<IMapper>(AutoMapperConfig.Mapper());
            services.AddSingleton<IJwtHandler, JwtHandler>(sp => new JwtHandler(jwtSettings));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IDistributedCache, MemoryDistributedCache>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddTransient<CancellationTokenMiddleware>();
            services.AddTransient<ICancellationTokenService, CancellationTokenService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<IRefreshTokenRepository, RefreshRepository>();
            services.AddSingleton<IEncrypter, Encrypter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });

            if (env.IsDevelopment())
            {
                
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Serwer.Api v1"));

            }


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

           
        }
    }
}
