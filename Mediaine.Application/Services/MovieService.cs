using AutoMapper;
using Mediaine.Application.Abstractions.Persistence;
using Mediaine.Application.Abstractions.Services;
using Mediaine.Application.Common.Models;
using Mediaine.Application.DTOs.Movie;
using Mediaine.Domain.Entities;

namespace Mediaine.Application.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public MovieService(
        IMovieRepository movieRepository,
        ICategoryRepository categoryRepository,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _movieRepository = movieRepository;
        _categoryRepository = categoryRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<MovieDto>> GetAllAsync(int page, int pageSize, string? search)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 10;

        var (items, totalData) = await _movieRepository.GetPagedAsync(page, pageSize, search);

        return new PaginationResponse<MovieDto>
        {
            Items = _mapper.Map<IReadOnlyList<MovieDto>>(items),
            CurrentPage = page,
            PageSize = pageSize,
            TotalData = totalData,
            TotalPages = (int)Math.Ceiling((double)totalData / pageSize),
            Search = search
        };
    }

    public async Task<MovieDto?> GetByIdAsync(int id)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        return movie is null ? null : _mapper.Map<MovieDto>(movie);
    }

    public async Task<MovieDto> CreateAsync(string title, decimal price, int categoryId, int userId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId)
            ?? throw new Exception("Category tidak ditemukan");

        var user = await _userRepository.GetByIdAsync(userId)
            ?? throw new Exception("User tidak ditemukan");

        var movie = new Movie
        {
            Title = title,
            Price = price,
            CategoryId = category.Id,
            UserId = user.Id
        };

        var created = await _movieRepository.AddAsync(movie);
        var result = await _movieRepository.GetByIdAsync(created.Id) ?? created;

        return _mapper.Map<MovieDto>(result);
    }

    public async Task<MovieDto?> UpdateAsync(int id, string title, decimal price, int categoryId, int userId)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        if (movie is null) return null;

        var category = await _categoryRepository.GetByIdAsync(categoryId)
            ?? throw new Exception("Category tidak ditemukan");

        var user = await _userRepository.GetByIdAsync(userId)
            ?? throw new Exception("User tidak ditemukan");

        movie.Title = title;
        movie.Price = price;
        movie.CategoryId = category.Id;
        movie.UserId = user.Id;

        await _movieRepository.UpdateAsync(movie);

        var updated = await _movieRepository.GetByIdAsync(movie.Id) ?? movie;
        return _mapper.Map<MovieDto>(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        if (movie is null) return false;

        await _movieRepository.DeleteAsync(movie);
        return true;
    }
}