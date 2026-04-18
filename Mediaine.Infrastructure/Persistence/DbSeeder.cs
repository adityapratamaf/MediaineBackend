using Mediaine.Domain.Constants;
using Mediaine.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mediaine.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(MediaineDbContext context)
    {
        await context.Database.MigrateAsync();

        if (!await context.Users.AnyAsync())
        {
            var admin = new User
            {
                Name = "Super Admin",
                Email = "admin@mediaine.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                Role = RoleConstants.Admin
            };

            var user1 = new User
            {
                Name = "Aditya User",
                Email = "user@mediaine.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("User123!"),
                Role = RoleConstants.User
            };

            var user2 = new User
            {
                Name = "Budi User",
                Email = "budi@mediaine.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("User123!"),
                Role = RoleConstants.User
            };

            await context.Users.AddRangeAsync(admin, user1, user2);
            await context.SaveChangesAsync();
        }

        if (!await context.Categories.AnyAsync())
        {
            var categories = new List<Category>
            {
                new() { Name = "Action" },
                new() { Name = "Drama" },
                new() { Name = "Comedy" }
            };

            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();
        }

        if (!await context.Movies.AnyAsync())
        {
            var admin = await context.Users.FirstAsync(x => x.Email == "admin@mediaine.com");
            var action = await context.Categories.FirstAsync(x => x.Name == "Action");
            var drama = await context.Categories.FirstAsync(x => x.Name == "Drama");

            var movies = new List<Movie>
            {
                new()
                {
                    Title = "John Wick",
                    Price = 50000,
                    CategoryId = action.Id,
                    UserId = admin.Id
                },
                new()
                {
                    Title = "The Pursuit of Happyness",
                    Price = 40000,
                    CategoryId = drama.Id,
                    UserId = admin.Id
                }
            };

            await context.Movies.AddRangeAsync(movies);
            await context.SaveChangesAsync();
        }
    }
}