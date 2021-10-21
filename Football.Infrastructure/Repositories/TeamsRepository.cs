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

        public async Task UpdateTeamsInfo(List<Team> teams)
        {
            IQueryable<Team> toUpdate = _dbContext.Teams;
            int i = 0;
            foreach (var team in toUpdate)
            {
                foreach (var t in teams)
                {
                    if (team.TeamName == t.TeamName)
                    {
                        team.GoalsScoredPerMatch = t.GoalsScoredPerMatch;
                        team.GoalsConcededPerMacth = t.GoalsConcededPerMacth;
                        team.AvgPossession = t.AvgPossession;
                        team.Standing = t.Standing;
                        team.Matches = t.Matches;
                        team.Points = t.Points;
                        team.Wins = team.Wins;
                        team.Draws = team.Draws;
                        team.Losses = team.Losses;
                        team.GoalBalance = team.GoalBalance;
                        team.GoalScored = team.GoalScored;
                        team.GoalConceded = team.GoalConceded;
                        team.MacthesHistory = team.MacthesHistory;
                        team.CleanSheets = team.CleanSheets;
                        team.AvgGoalsPerMacth = team.AvgGoalsPerMacth;
                        i++;
                        break;
                    }
                }
            }

            var result = await _dbContext.SaveChangesAsync();

            if (result != teams.Count) throw new NotFoundException("Error.");
        }
    }
}
