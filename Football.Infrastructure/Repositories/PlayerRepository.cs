using Football.Application.Contracts.Persistence;
using Football.Domain.Entities;
using Football.Infrastructure.Exception;
using Football.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Football.Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly FootballDbContext _dbContext;

        public PlayerRepository(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Player>> GetPlayersByLeagueName(int  id)
        {
            var players = await _dbContext.Players
                .Where(p => p.Team.League.Id == id)
                .ToListAsync();

            if (players.Count == 0) throw new NotFoundException("Players not found.");

            return players;
        }

        public async Task<IReadOnlyList<Player>> GetBestScorersForLeague(int id)
        {
            var players = await _dbContext.Players
                .Where(p => p.Team.League.Id == id)
                .OrderByDescending(p => p.Goals)
                .Take(15)
                .ToListAsync();

            if (players.Count == 0) throw new NotFoundException("Players not found.");

            return players;
        }
        public async Task<IReadOnlyList<Player>> GetPlayersByTeamName(int id)
        {
            var players =  await _dbContext.Players
                .Where(p => p.Team.Id == id)
                .ToListAsync();

            if (players.Count == 0) throw new NotFoundException("Players not found.");

            return players;
        }
        public async Task<Player> GetPlayer(int id)
        {
            var player =  await _dbContext.Players
                .FindAsync(id);

            if (player == null) throw new NotFoundException("Player not found.");

            return player;
        }

        public async Task UpdatePlayersInfo(List<Player> players)
        {
            IQueryable<Player> toUpdate = _dbContext.Players;
            foreach (var player in toUpdate)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    if (player.Name == players[i].Name)
                    {
                        player.MatchesPlayed = players[i].MatchesPlayed;
                        player.MinutesPlayed = players[i].MinutesPlayed;
                        player.StartedFromBegin = players[i].StartedFromBegin;
                        player.Goals = players[i].Goals;
                        player.Assists = players[i].Assists;
                        player.RedCards = players[i].RedCards;
                        player.YellowCards = players[i].YellowCards;
                    }
                }
            }

            var result = await _dbContext.SaveChangesAsync();

            if (result != players.Count) throw new NotFoundException("Error.");
        }
    }
}
