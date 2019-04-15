using Microsoft.EntityFrameworkCore;
using Domain.Helpers;
using Models.Models;

namespace Domain.Context
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<ImageModel> Images { get; set; }
        public DbSet<TagModel> Tags { get; set; }
        public DbSet<ScoreModel> Scores { get; set; }
        public DbSet<UserToImageScore> UserToImageScores { get; set; }
        public DbSet<UserToImageTag> UserToImageTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserToImageTag>()
                .HasOne(uit => uit.Image)
                .WithMany(i => i.UserToImageTags)
                .HasForeignKey(uit => uit.ImageId);
            modelBuilder.Entity<UserToImageTag>()
                .HasOne(uit => uit.Tag)
                .WithMany(t => t.UserToImageTags)
                .HasForeignKey(uit => uit.TagId);
            modelBuilder.Entity<UserToImageTag>()
                .HasOne(uit=>uit.User)
                .WithMany(u => u.UserToImageTags)
                .HasForeignKey(uit => uit.UserId);

            modelBuilder.Entity<UserToImageScore>()
                .HasOne(uis => uis.Image)
                .WithMany(i => i.UserToImageScores)
                .HasForeignKey(uis => uis.ImageId);
            modelBuilder.Entity<UserToImageScore>()
                .HasOne(uis => uis.Score)
                .WithMany(s => s.UserToImageScores)
                .HasForeignKey(uis => uis.ScoreId);
            modelBuilder.Entity<UserToImageScore>()
                .HasOne(uis => uis.User)
                .WithMany(u => u.UserToImageScores)
                .HasForeignKey(uis => uis.UserId);

            modelBuilder.Entity<RoleModel>().HasData(
                new RoleModel() { Id = 1, Name = "admin" },
                new RoleModel() { Id = 2, Name = "user" }
            );

            modelBuilder.Entity<UserModel>().HasData(
                new UserModel() { Id = 1, UserName = "admin", Password = AuthenticationHelper.HashPassword("123"), RoleId = 1 },
                new UserModel() { Id = 2, UserName = "user", Password = AuthenticationHelper.HashPassword("123"), RoleId = 2 }
            );
            modelBuilder.Entity<ScoreModel>().HasData(
                new ScoreModel() { Id = 1, Value = 1 },
                new ScoreModel() { Id = 2, Value = 2 },
                new ScoreModel() { Id = 3, Value = 3 },
                new ScoreModel() { Id = 4, Value = 4 },
                new ScoreModel() { Id = 5, Value = 5 }
            );
        }
    }
}
