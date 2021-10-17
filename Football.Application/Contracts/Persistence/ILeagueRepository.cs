using Football.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Application.Contracts.Persistence
{
    public interface ILeagueRepository
    {
        public Task<IReadOnlyList<League>> GetLeagues();
        public Task<League> GetLeagueById(int id);
        public Task<IReadOnlyList<Round>> GetScheduleByLeague(int id);
        public  Task<IReadOnlyList<Match>> GetScheduleByDate(int id, string date);
    }
}
