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
    }
}
