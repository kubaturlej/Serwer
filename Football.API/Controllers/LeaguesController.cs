using Football.Application.Features.Leagues.Commands;
using Football.Application.Features.Leagues.Queries.GetLeagues;
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

        //[Authorize(Roles = "Admin")]
        [HttpPatch("leagues")]
        public async Task<ActionResult> UpdateLeagueInfo()
        {
            var result = UpdateService.GetLeaguesInfoForUpdate();
            await Mediator.Send(new UpdateLeagues.Command { Leagues = result });

            return Ok();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPatch("matches")]
        public async Task<ActionResult> UpdateMatchesInfo()
        {
            var result = UpdateService.GetMatchInfoForUpdate();
            await Mediator.Send(new UpdateMatches.Command { Matches = result });

            return Ok();
        }

    }
}
