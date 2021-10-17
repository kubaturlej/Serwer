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
    public class LeagueRepository : ILeagueRepository
    {
        private readonly FootballDbContext _dbContext;

        public LeagueRepository(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<League>> GetLeagues()
        {
            var leagues =  await _dbContext.Leagues
                .ToListAsync();

            if (leagues.Count == 0) throw new NotFoundException("Leagues not found.");

            return leagues;
        }

        public async Task<League> GetLeagueById(int id)
        {
            var league =  await _dbContext.Leagues
                .FindAsync(id);

            if (league == null) throw new NotFoundException("League not found.");

            return league;
        }

        public async Task<IReadOnlyList<Round>> GetScheduleByLeague(int id)
        {
            var schedule =  await _dbContext.Rounds
                .Include(m => m.Matches)
                .Where(l => l.League.Id == id)
                .ToListAsync();

            if (schedule.Count == 0) throw new NotFoundException("Schedule not found.");

            return schedule;
        }

        public async Task<IReadOnlyList<Match>> GetScheduleByDate(int id, string date)
        {
            var matches = await _dbContext.Matches
                .Where(m => m.Round.League.Id == id)
                .Where(m => m.Date == date)
                .ToListAsync();

            if (matches.Count == 0) throw new NotFoundException("Matches not found.");

            return matches;
        }
    }
}
