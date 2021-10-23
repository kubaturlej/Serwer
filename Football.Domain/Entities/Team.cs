using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Domain.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string TeamNationality { get; set; }
        public string GoalsScoredPerMatch { get; set; }
        public string GoalsConcededPerMacth { get; set; }
        public string AvgPossession { get; set; }
        public int Standing { get; set; }
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
        public virtual List<Player> Players { get; set; }
        public virtual League League { get; set; }
    }
}

