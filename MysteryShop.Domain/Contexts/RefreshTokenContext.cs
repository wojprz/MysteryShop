using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MysteryShop.Domain.Entities;
using MysteryShop.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace MysteryShop.Domain.Contexts
{
    public class RefreshTokenContext : DbContext
    {
        private readonly SqlSettings _settings;

        public RefreshTokenContext(DbContextOptions<RefreshTokenContext> options, IOptions<SqlSettings> sqlSettings) : base(options)
        {
            _settings = sqlSettings.Value;
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("RefreshTokens");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var itemBuilder = modelBuilder.Entity<RefreshToken>();
            itemBuilder.HasKey(x => x.Id);
            modelBuilder.Entity<RefreshToken>().ToTable("RefreshTokens");
        }
    }
}
