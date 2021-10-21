using Football.API.Services;
using Football.Application.Features.Leagues.Commands;
using Football.Application.Features.Players;
using Football.Application.Features.Teams.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Football.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IUpdateDatabaseService _updateDatabaseService;
        private readonly IMediator _mediator;

        public AdminController(IUpdateDatabaseService updateDatabaseService, IMediator mediator)
        {
            _updateDatabaseService = updateDatabaseService;
            _mediator = mediator;
        }
        //[Authorize(Roles = "Admin")]
        [HttpPatch("leagues")]
        public async Task<ActionResult> UpdateLeagueInfo()
        {
            var result = _updateDatabaseService.GetLeaguesInfoForUpdate();
            await _mediator.Send(new UpdateLeagues.Command { Leagues = result });

            return Ok();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPatch("teams")]
        public async Task<ActionResult> UpdateTeamsInfo()
        {
            var result = _updateDatabaseService.GetTeamsInfoForUpdate();
            await _mediator.Send(new UpdateTeams.Command { Teams = result });

            return Ok();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPatch("players")]
        public async Task<ActionResult> UpdatePlayersInfo()
        {
            var result = _updateDatabaseService.GetPlayersInfoForUpdate();
            await _mediator.Send(new UpdatePlayers.Command { Players = result });

            return Ok();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPatch("matches")]
        public async Task<ActionResult> UpdateMatchesInfo()
        {
            var result = _updateDatabaseService.GetMatchInfoForUpdate();
            await _mediator.Send(new UpdateMatches.Command { Matches = result });

            return Ok();
        }
    }
}
