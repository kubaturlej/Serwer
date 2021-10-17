using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataScraper.Models
{
    public class Round
    {
        public ICollection<Match> Matches { get; set; } = new List<Match>();
        public string RoundNumber { get; set; }
    }
}
