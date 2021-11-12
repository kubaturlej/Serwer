using AutoMapper;
using Football.Application.Contracts.Persistence;
using Football.Application.Features.Leagues.Queries.GetPlayers;
using Football.Application.Features.Leagues.Queries.GetTeams;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Football.Application.Features.Leagues.Queries.GetLeagues
{
    public class GetLeagues
    {
        public class Query : IRequest<List<LeagueDto>>
        {
            public string Date { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<LeagueDto>>
        {
            private readonly ILeagueRepository _leagueRepository;
            private readonly IMapper _mapper;

            public Handler(ILeagueRepository leagueRepository, IMapper mapper)
            {
                _leagueRepository = leagueRepository;
                _mapper = mapper;
            }
            public async Task<List<LeagueDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var leagues = await _leagueRepository.GetLeagues();

                var listWithMatches =  _mapper.Map<List<LeagueDto>>(leagues);
                foreach (var league in listWithMatches)
                {
                    var matches = await _leagueRepository.GetScheduleByDate(league.Id, request.Date);
                    league.Matches = _mapper.Map<List<MatchDto>>(matches);
                }

                return listWithMatches;

            }
        }
    }
}
