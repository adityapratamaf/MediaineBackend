using AutoMapper;
using Mediaine.Application.DTOs.Movie;
using Mediaine.Application.DTOs.Category;
using Mediaine.Application.DTOs;
using Mediaine.Domain.Entities;

namespace Mediaine.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();

        CreateMap<Category, CategoryDto>();

        CreateMap<Movie, MovieDto>()
            .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty))
            .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => src.User != null ? src.User.Name : string.Empty))
            .ForMember(dest => dest.ImageUrl, 
                opt => opt.MapFrom(src => src.FilePath));
    }
}