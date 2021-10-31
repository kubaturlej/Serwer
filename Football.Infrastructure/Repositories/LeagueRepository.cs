using AutoMapper;
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
        private readonly IMapper _mapper;

        public LeagueRepository(FootballDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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

            if (matches.Count == 0) return new List<Match>();

            return matches;
        }

        public async Task UpdateLegaueInfo(List<League> leagues)
        {
           IQueryable<League> toUpdate = _dbContext.Leagues;
           foreach (var league in toUpdate)
           {
                for (int i = 0; i < leagues.Count; i++)
                {
                    if (league.LeagueName == leagues[i].LeagueName)
                    {
                        league.LeagueProgress = leagues[i].LeagueProgress;
                        league.MatchesCompleted = leagues[i].MatchesCompleted;
                    }
                }
           }

            var result = await _dbContext.SaveChangesAsync();
               
            //if (result != leagues.Count) throw new NotFoundException("Error.");
        }

        public async Task UpdateMatchesInfo(List<Match> matches)
        {
            IQueryable<Match> toUpdate = _dbContext.Matches;

            foreach (var match in toUpdate)
            {
                for (int i = 0; i < matches.Count; i++)
                {
                    if (match.Date == matches[i].Date && match.Time == matches[i].Time && (match.FirstTeam == matches[i].FirstTeam || match.SecondTeam == matches[i].SecondTeam))
                    {
                        match.Score = matches[i].Score;
                    }
                }
            }

            var result = await _dbContext.SaveChangesAsync();

            //if (result != matches.Count) throw new NotFoundException("Error.");
        }    
    }
}
