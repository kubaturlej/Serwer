using Football.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Application.Contracts.Persistence
{
    public interface ITeamsRepository
    {
        public Task<Team> GetTeam(int id);
        public Task<IReadOnlyList<Team>> GetTeamsByLeagueName(int id);

    }
}
