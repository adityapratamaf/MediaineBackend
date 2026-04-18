using AutoMapper;
using Mediaine.Application.DTOs;
using Mediaine.Application.Interfaces;
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

    public async Task<List<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<List<UserDto>>(users);
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

        var created = await _userRepository.CreateAsync(user);
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