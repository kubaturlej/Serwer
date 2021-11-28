using Football.Application.Contracts.Persistence;
using Football.Domain.Entities;
using Football.Infrastructure.Exception;
using Football.Infrastructure.Exceptions;
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
                .Include(t => t.Players)
                .FirstOrDefaultAsync(t => t.Id == id);

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

        public async Task<IReadOnlyList<Team>> GetTeamsByName(string name)
        {
            var teams = await _dbContext.Teams.ToListAsync();
            var result = teams
                .Where(t => string.Equals(name, t.TeamName, StringComparison.OrdinalIgnoreCase) || t.TeamName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
              
              
            if (result.Count == 0) throw new NoResultsException("Teams not found.");

            return result;
        }

        public async Task<IReadOnlyList<Match>> GetTeamSchedule(string name)
        {
            var tokens = name.Split(' ');
            var matches = new List<Match>();
            var newTokens = new List<string>();
            int start = 0, end = tokens.Length;

            string tryName = default(string);
            int counter = 0;
            do
            {
                if (counter % 2 == 0)
                {
                    for (int i = 0; i < end; i++)
                    {
                        newTokens.Add(tokens[i]);
                    }
                    end--;
                }
                else
                {
                    for (int i = start; i < tokens.Length; i++)
                    {
                        newTokens.Add(tokens[i]);
                    }
                    start++;
                }

                counter++;

                tryName = string.Join(' ', newTokens);
                matches = await _dbContext.Matches
                    .Where(m => m.FirstTeam == tryName || m.SecondTeam == tryName)
                    .ToListAsync();

                newTokens.Clear();
            } 
            while (matches.Count == 0);


            matches.Sort((x, y) => DateTime.Parse(x.Date).CompareTo(DateTime.Parse(y.Date)));

            if (matches.Count == 0) throw new NotFoundException("Matches not found.");

            return matches;
        }
        public async Task<IReadOnlyList<Match>> GetTeamMatchForSpecificDay(string teamName, string date)
        {
            var matches = await GetTeamSchedule(teamName);

            return matches.Where(m => m.Date == date).ToList();
        }

        public async Task<IReadOnlyList<Team>> GetFavoritesTeams(int userId)
        {
            var favorites = await _dbContext.Favorites
                .Where(f => f.UserId == userId)
                .ToListAsync();

            var favoriteTeams = new List<Team>();

            foreach (var item in favorites)
            {
                favoriteTeams.Add(_dbContext.Teams.Include(t => t.Players).FirstOrDefault(t => t.Id == item.TeamId));
            }

            return favoriteTeams;
        }

        public async Task HandleFavoriteTeam(int userId, int teamId)
        {
            var result = await _dbContext.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.TeamId == teamId);

            if (result == null)
            {
                _dbContext.Favorites
                    .Add(new Favorite { UserId = userId, TeamId = teamId });
            } 
            else
            {
                _dbContext.Favorites
                    .Remove(result);
            }

            await _dbContext.SaveChangesAsync();
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
