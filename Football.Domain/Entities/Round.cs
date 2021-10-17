using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Domain.Entities
{
    public class Round
    {
        public int Id { get; set; }
        public string RoundNumber { get; set; }
        public virtual List<Match> Matches { get; set; }
        public virtual League League{ get; set; }
    }
}
