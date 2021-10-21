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
            int i = 0;
            foreach (var player in toUpdate)
            {
                foreach (var p in players)
                {
                    if (player.Name == p.Name)
                    {
                        player.MatchesPlayed = p.MatchesPlayed;
                        player.MinutesPlayed = p.MinutesPlayed;
                        player.StartedFromBegin = p.StartedFromBegin;
                        player.Goals = p.Goals;
                        player.Assists = p.Assists;
                        player.RedCards = p.RedCards;
                        player.YellowCards = p.YellowCards;
                        i++;
                        break;
                    }
                }
            }

            var result = await _dbContext.SaveChangesAsync();

            if (result != players.Count) throw new NotFoundException("Error.");
        }
    }
}
