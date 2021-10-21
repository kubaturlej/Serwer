using Football.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Application.Contracts.Persistence
{
    public interface IPlayerRepository
    {
        public Task<IReadOnlyList<Player>> GetPlayersByLeagueName(int id);
        public Task<IReadOnlyList<Player>> GetPlayersByTeamName(int id);
        public Task<Player> GetPlayer(int id);
        public Task UpdatePlayersInfo(List<Player> players);
    }
}
