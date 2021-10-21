using Football.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Football.Persistence
{
    public class FootballDbContext : DbContext
    {
        public FootballDbContext(DbContextOptions<FootballDbContext> options) : base(options)
        {

        }

        public DbSet<League> Leagues { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
