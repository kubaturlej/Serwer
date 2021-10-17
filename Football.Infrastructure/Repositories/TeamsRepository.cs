using Football.Application.Contracts.Persistence;
using Football.Domain.Entities;
using Football.Infrastructure.Exception;
using Football.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Infrastructure.Repositories
{
    public class TeamsRepository : ITeamsRepository
    {
        private readonly FootballDbContext _dbContext;

        public TeamsRepository(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Team> GetTeam(int id)
        {    
            var team =  await _dbContext.Teams
                .FindAsync(id);

            if (team == null) throw new NotFoundException("Team not found.");

            return team;
        }

        public async Task<IReadOnlyList<Team>> GetTeamsByLeagueName(int id)
        {
            var teams =  await _dbContext.Teams
                .Where(t => t.League.Id == id)
                .ToListAsync();

            if (teams.Count == 0) throw new NotFoundException("Teams not found.");

            return teams;
        }
    }
}
