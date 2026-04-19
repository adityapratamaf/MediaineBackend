using FluentValidation;
using MediatR;
using Mediaine.Application.Abstractions.Services;
using Mediaine.Application.Behaviors;
using Mediaine.Application.Mapping;
using Mediaine.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Mediaine.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}