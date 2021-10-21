using Football.Application.Features.Leagues.Queries.GetLeagues;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Football.API.Controllers
{
    public class LeaguesController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeagueDto>>> GetLeagues()
        {
            var result = await Mediator.Send(new GetLeagues.Query());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<LeagueDto>>> GetLeagueById(int id)
        {
            var result = await Mediator.Send(new GetLeagueById.Query {Id = id });
            return Ok(result);
        }

        [HttpGet("{leagueId}/schedule")]
        public async Task<ActionResult<IEnumerable<RoundDto>>> GetScheduleForLeague(int leagueId)
        {
            var result = await Mediator.Send(new GetScheduleByLeague.Query { Id = leagueId });
            return Ok(result);
        }

        [HttpGet("{leagueId}/schedule/date")]
        public async Task<ActionResult<IEnumerable<RoundDto>>> GetScheduleByDate(int leagueId, [FromHeader] string dateTime)
        {
            var result = await Mediator.Send(new GetScheduleByDate.Query { Id = leagueId, Date = dateTime });
            return Ok(result);
        }
        
    }
}
