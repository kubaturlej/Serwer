using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Domain.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string FirstTeam { get; set; }
        public string SecondTeam { get; set; }
        public string Score { get; set; }
        public Round Round { get; set; }
    }
}
