using System;
using canalTransacao.Entities;
using Microsoft.EntityFrameworkCore;

namespace canalTransacao.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {
        }

        public DbSet<Registry> Registry { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RegistryConfiguration());
        }
    }
}