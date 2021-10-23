using FluentValidation;
using FluentValidation.AspNetCore;
using Football.API.DTOs;
using Football.API.DTOs.Validators;
using Football.API.Middleware;
using Football.API.Services;
using Football.Application.Contracts.Persistence;
using Football.Application.Features.Leagues.Queries.GetLeagues;
using Football.Application.Mapping;
using Football.Domain.Entities;
using Football.Infrastructure.Repositories;
using Football.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Football.API.Extensions
{
    public static class ServiceExtenions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
                                                        IConfiguration configuration)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Football.API", Version = "v1" });
            });

            services.AddDbContext<FootballDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("FootballDbConnection"));
            });
            services.AddAutoMapper(typeof(Profiles).Assembly);
            services.AddMediatR(typeof(GetLeagues.Query).Assembly);
            services.AddScoped<ILeagueRepository, LeagueRepository>();
            services.AddScoped<ITeamsRepository, TeamsRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IUpdateDatabaseService, UpdateDatabaseService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ErrorHandleMiddleware>();

            services.AddCors(options =>
            {
                options.AddPolicy("FrontEndClient", builer =>
                    builer.AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins(configuration["AllowedOrigins"])
                );
            });

            return services;
        }

        public static IServiceCollection AddSecurityServices(this IServiceCollection services,
                                                IConfiguration configuration)
        {
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            //services.AddScoped<IAuthorizationHandler, IsAdminRequirementHandler>();
            services.AddControllers().AddFluentValidation();
            services.AddScoped<IValidator<RegisterDto>, RegisterDtoValidator>();
            return services;
        }
    }
}
