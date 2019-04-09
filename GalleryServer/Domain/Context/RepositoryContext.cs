using Microsoft.EntityFrameworkCore;
using Domain.Helpers;
using Models.Models;

namespace Domain.Context
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Role>().HasData(
                new Role() { Id = 1, Name = "admin" },
                new Role() { Id = 2, Name = "user" }
            );

            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, UserName = "admin", Password = AuthenticationHelper.HashPassword("123"), RoleId = 1 },
                new User() { Id = 2, UserName = "user", Password = AuthenticationHelper.HashPassword("123"), RoleId = 2 }
            );
        }
    }
}
