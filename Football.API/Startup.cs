using Football.API.Middleware;
using Football.Application.Contracts.Persistence;
using Football.Application.Features.Leagues.Queries.GetLeagues;
using Football.Application.Mapping;
using Football.Infrastructure.Repositories;
using Football.Persistence;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;

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
            services.AddScoped<ErrorHandleMiddleware>();
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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
