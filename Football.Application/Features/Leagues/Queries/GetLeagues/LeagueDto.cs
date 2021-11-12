using Football.Application.Features.Leagues.Queries.GetPlayers;
using Football.Application.Features.Leagues.Queries.GetTeams;
using Football.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Application.Features.Leagues.Queries.GetLeagues
{
    public class LeagueDto
    {
        public int Id { get; set; }
        public string LeagueName { get; set; }
        public string TotalMatches { get; set; }
        public string MatchesCompleted { get; set; }
        public string LeagueProgress { get; set; }
        public string Logo { get; set; }
        public string Nationality { get; set; }
        public ICollection<MatchDto> Matches { get; set; }
        public ICollection<PlayerDto> Scorers { get; set; }
        public ICollection<TeamDto> Teams { get; set; }
        public ICollection<RoundDto> Rounds { get; set; }

    }
}
