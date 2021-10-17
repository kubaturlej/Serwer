using Football.Application.Features.Leagues.Queries.GetTeams;
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
    }
}
