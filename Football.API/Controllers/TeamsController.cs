using Football.Application.Features.Leagues.Queries.GetLeagues;
using Football.Application.Features.Leagues.Queries.GetTeams;
using Football.Application.Features.Teams.Commands;
using Football.Application.Features.Teams.Queries.GetTeams;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Football.API.Controllers
{
    public class TeamsController : BaseApiController
    {
        [HttpGet("{leagueId}/league")]
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetTeamsByLeagueId(int leagueId)
        {
            var result = await Mediator.Send(new GetTeamsByLeagueName.Query() { Id = leagueId });
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDto>> GetTeamById(int id)
        {
            var result = await Mediator.Send(new GetTeam.Query() { Id = id });
            return Ok(result);
        }

        [HttpGet()]
        public async Task<ActionResult<TeamDto>> GetTeamByName([FromHeader] string teamName)
        {
           var result = await Mediator.Send(new GetTeamByName.Query() { Name = teamName });
            return Ok(result);
        }

        [HttpGet("schedule")]
        public async Task<ActionResult<MatchDto>> GetTeamSchedule([FromHeader] string teamName)
        {
            var result = await Mediator.Send(new GetTeamSchedule.Query() { Name = teamName });
            return Ok(result);
        }


        //[Authorize(Roles = "Admin")]
        [HttpPatch("teams")]
        public async Task<ActionResult> UpdateTeamsInfo()
        {
            var result = UpdateService.GetTeamsInfoForUpdate();
            await Mediator.Send(new UpdateTeams.Command { Teams = result });

            return Ok();
        }
    }
}
