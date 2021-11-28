using Football.Application.Features.Leagues.Queries.GetLeagues;
using Football.Application.Features.Leagues.Queries.GetPlayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Application.Features.Leagues.Queries.GetTeams
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string TeamNationality { get; set; }
        public string GoalsScoredPerMatch { get; set; }
        public string GoalsConcededPerMacth { get; set; }
        public string AvgPossession { get; set; }
        public string Standing { get; set; }
        public string Matches { get; set; }
        public string Points { get; set; }
        public string Wins { get; set; }
        public string Draws { get; set; }
        public string Losses { get; set; }
        public string GoalBalance { get; set; }
        public string GoalScored { get; set; }
        public string GoalConceded { get; set; }
        public string MacthesHistory { get; set; }
        public string CleanSheets { get; set; }
        public string AvgGoalsPerMacth { get; set; }
        public string Logo { get; set; }
        public ICollection<PlayerDto> Players { get; set; }
        public ICollection<MatchDto> Schedule { get; set; }
    }
}
