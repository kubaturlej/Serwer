using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataScraper.Models
{
    public class Player
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public string Nationality { get; set; }
        public string Position { get; set; }
        public string MatchesPlayed { get; set; }
        public string MinutesPlayed { get; set; }
        public string StartedFromBegin { get; set; }
        public int Goals { get; set; }
        public string Assists { get; set; }
        public string RedCards { get; set; }
        public string YellowCards { get; set; }
    }
}
