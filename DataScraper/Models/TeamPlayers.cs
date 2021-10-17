using DataScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataScraper.Models
{
    public class TeamPlayers
    {
        public ICollection<Player> Players { get; set; } = new List<Player>();
    }
}
