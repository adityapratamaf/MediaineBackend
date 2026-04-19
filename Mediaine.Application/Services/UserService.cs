using AutoMapper;
using Mediaine.Application.DTOs;
using Mediaine.Application.Abstractions.Services;
using Mediaine.Application.Abstractions.Security;
using Mediaine.Application.Common.Models;
using Mediaine.Application.Abstractions.Persistence;
using Mediaine.Domain.Entities;

namespace Mediaine.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;

    public UserService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }

    // ✅ PAGINATION + SEARCH
    public async Task<PaginationResponse<UserDto>> GetAllAsync(int page, int pageSize, string? search)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 10;

        var (items, totalData) = await _userRepository.GetPagedAsync(page, pageSize, search);

        return new PaginationResponse<UserDto>
        {
            Items = _mapper.Map<IReadOnlyList<UserDto>>(items),
            CurrentPage = page,
            PageSize = pageSize,
            TotalData = totalData,
            TotalPages = (int)Math.Ceiling((double)totalData / pageSize),
            Search = search
        };
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user is null ? null : _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> CreateAsync(string name, string email, string password, string role)
    {
        var existingUser = await _userRepository.GetByEmailAsync(email);
        if (existingUser is not null)
            throw new Exception("Email sudah terdaftar");

        var user = new User
        {
            Name = name,
            Email = email,
            PasswordHash = _passwordHasher.HashPassword(password),
            Role = role
        };

        var created = await _userRepository.AddAsync(user);
        return _mapper.Map<UserDto>(created);
    }

    public async Task<UserDto?> UpdateAsync(int id, string name, string email, string role)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null) return null;

        var emailOwner = await _userRepository.GetByEmailAsync(email);
        if (emailOwner is not null && emailOwner.Id != id)
            throw new Exception("Email sudah digunakan user lain");

        user.Name = name;
        user.Email = email;
        user.Role = role;

        await _userRepository.UpdateAsync(user);

        return _mapper.Map<UserDto>(user);
    }
}