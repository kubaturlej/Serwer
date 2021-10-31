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

            foreach (var team in toUpdate)
            {
                for (int i = 0; i < teams.Count; i++)
                {
                    if (team.TeamName == teams[i].TeamName)
                    {
                        team.GoalsScoredPerMatch = teams[i].GoalsScoredPerMatch;
                        team.GoalsConcededPerMacth = teams[i].GoalsConcededPerMacth;
                        team.AvgPossession = teams[i].AvgPossession;
                        team.Standing = teams[i].Standing;
                        team.Matches = teams[i].Matches;
                        team.Points = teams[i].Points;
                        team.Wins = teams[i].Wins;
                        team.Draws = teams[i].Draws;
                        team.Losses = teams[i].Losses;
                        team.GoalBalance = teams[i].GoalBalance;
                        team.GoalScored = teams[i].GoalScored;
                        team.GoalConceded = teams[i].GoalConceded;
                        team.MacthesHistory = teams[i].MacthesHistory;
                        team.CleanSheets = teams[i].CleanSheets;
                        team.AvgGoalsPerMacth = teams[i].AvgGoalsPerMacth;
                    }
                }
            }

            var result = await _dbContext.SaveChangesAsync();

            //if (result != teams.Count) throw new NotFoundException("Error.");
        }
    }
}
