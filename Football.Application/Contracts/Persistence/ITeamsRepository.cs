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
        public Task<IReadOnlyList<Match>> GetTeamSchedule(string name);
        public Task<IReadOnlyList<Team>> GetTeamsByName(string name);
        public Task HandleFavoriteTeam(int userId, int teamId);
        public Task<IReadOnlyList<Team>> GetFavoritesTeams(int userId);
        public Task<IReadOnlyList<Match>> GetTeamMatchForSpecificDay(string teamName, string date);
        public Task UpdateTeamsInfo(List<Team> teams);
    }
}
