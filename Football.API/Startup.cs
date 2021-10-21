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
using Football.Infrastructure.Authorization;
using Football.Infrastructure.Repositories;
using Football.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace Football.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Football.API", Version = "v1" });
            });

            services.AddDbContext<FootballDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("FootballDbConnection"));
            });
            services.AddAutoMapper(typeof(Profiles).Assembly);
            services.AddMediatR(typeof(GetLeagues.Query).Assembly);
            services.AddScoped<ILeagueRepository, LeagueRepository>();
            services.AddScoped<ITeamsRepository, TeamsRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ErrorHandleMiddleware>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"]));

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
            services.AddCors(options =>
            {
                options.AddPolicy("FrontEndClient", builer =>
                    builer.AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins(Configuration["AllowedOrigins"])
                );
            });
            //services.AddControllers().AddNewtonsoftJson(options =>
            //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Football.API v1"));
            }

            app.UseMiddleware<ErrorHandleMiddleware>();
            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("FrontEndClient");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
