using Mediaine.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mediaine.Infrastructure.Persistence;

public class MediaineDbContext : DbContext
{
    public MediaineDbContext(DbContextOptions<MediaineDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Movie> Movies => Set<Movie>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(x => x.Name).IsRequired().HasMaxLength(100);
            entity.Property(x => x.Email).IsRequired().HasMaxLength(150);
            entity.HasIndex(x => x.Email).IsUnique();
            entity.Property(x => x.PasswordHash).IsRequired();
            entity.Property(x => x.Role).IsRequired().HasMaxLength(20);
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.Property(x => x.Token).IsRequired();
            entity.HasIndex(x => x.Token).IsUnique();

            entity.HasOne(x => x.User)
                .WithMany(x => x.RefreshTokens)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(x => x.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.Property(x => x.Title).IsRequired().HasMaxLength(100);
            entity.Property(x => x.Price).HasColumnType("decimal(18,2)");

            entity.HasOne(x => x.Category)
                .WithMany(x => x.Movies)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.User)
                .WithMany(x => x.Movies)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        base.OnModelCreating(modelBuilder);
    }
}