using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Application.Features.Leagues.Queries.GetLeagues
{
    public class RoundDto
    {
        public int Id { get; set; }
        public string RoundNumber { get; set; }
        public  ICollection<MatchDto> Matches { get; set; }
    }
}
