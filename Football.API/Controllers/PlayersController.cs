using Football.Application.Features.Leagues.Queries.GetPlayers;
using Football.Application.Features.Players;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Football.API.Controllers
{
    public class PlayersController : BaseApiController
    {
        [HttpGet("{leagueId}/league")]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetPlayersByLeagueId(int leagueId)
        {
            var result = await Mediator.Send(new GetPlayersByLeagueName.Query() { Id = leagueId });
            return Ok(result);
        }

        [HttpGet("{teamId}/team")]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetPlayersByTeamId(int teamId)
        {
            var result = await Mediator.Send(new GetPlayersByTeamName.Query() { Id = teamId });
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerDto>> GetPLayerById(int id)
        {
            var result = await Mediator.Send(new GetPlayer.Query() { Id = id });
            return Ok(result);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPatch("players")]
        public async Task<ActionResult> UpdatePlayersInfo()
        {
            var result = UpdateService.GetPlayersInfoForUpdate();
            await Mediator.Send(new UpdatePlayers.Command { Players = result });

            return Ok();
        }
    }
}
