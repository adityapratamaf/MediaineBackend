using Mediaine.Application.Abstractions.Persistence;
using Mediaine.Domain.Entities;
using Mediaine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Mediaine.Infrastructure.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly MediaineDbContext _context;

    public MovieRepository(MediaineDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Movie>> GetAllAsync()
    {
        return await _context.Movies
            .Include(x => x.Category)
            .Include(x => x.User)
            .ToListAsync();
    }

    public async Task<Movie?> GetByIdAsync(int id)
    {
        return await _context.Movies
            .Include(x => x.Category)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Movie> AddAsync(Movie movie)
    {
        await _context.Movies.AddAsync(movie);
        await _context.SaveChangesAsync();
        return movie;
    }

    public async Task UpdateAsync(Movie movie)
    {
        _context.Movies.Update(movie);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Movie movie)
    {
        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
    }
}