using Football.Application.Features.Leagues.Queries.GetLeagues;
using Football.Application.Features.Leagues.Queries.GetTeams;
using Football.Application.Features.Teams.Commands;
using Football.Application.Features.Teams.Queries.GetTeams;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
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


        [HttpGet("match")]
        public async Task<ActionResult<MatchDto>> GetTeamMatchForSpecificDay([FromHeader] string teamName, [FromHeader] string dateTime)
        {
            var result = await Mediator.Send(new GetTeamMatchForSpecificDay.Query() { Name = teamName, Date = dateTime });
            return Ok(result);
        }


        [HttpPost("favorite")]
        public async Task<ActionResult<MatchDto>> HandleFavoriteTeam([FromHeader] string teamId)
        {
            var userID = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var teamID = int.Parse(teamId);
            var result = await Mediator.Send(new HandleFavoriteTeam.Command { UserId = userID, TeamId = teamID });
            return Ok(result);
        }


        [HttpGet("favorite")] 
        public async Task<ActionResult<MatchDto>> GetFavoriteTeams()
        {
            var userID = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var result = await Mediator.Send(new GetFavoritesTeams.Query { UserId = userID });
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
