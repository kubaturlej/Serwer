using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataScraper.Models
{
    public class Match
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string FirstTeam { get; set; }
        public string SecondTeam { get; set; }
        public string Score { get; set; }
    }
}
