using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Domain.Entities
{
    public class League
    {
        public int Id { get; set; }
        public string LeagueName { get; set; }
        public string TotalMatches { get; set; }
        public string MatchesCompleted { get; set; }
        public string LeagueProgress { get; set; }
        public string Logo { get; set; }
        public string Nationality { get; set; }
        public virtual List<Team> Teams { get; set; }
        public virtual List<Round> Rounds { get; set; }
    }
}
