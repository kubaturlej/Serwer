using AutoMapper;
using Football.Application.Contracts.Persistence;
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
        }

        public class Handler : IRequestHandler<Query, List<LeagueDto>>
        {
            private readonly ILeagueRepository _repository;
            private readonly IMapper _mapper;

            public Handler(ILeagueRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }
            public async Task<List<LeagueDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var leagues = await _repository.GetLeagues();

                var listWithMatches =  _mapper.Map<List<LeagueDto>>(leagues);
                foreach (var league in listWithMatches)
                {
                    var matches = await _repository.GetScheduleByDate(league.Id, DateTime.Now.ToString("dd'/'MM'/'yyyy"));
                    league.Matches = _mapper.Map<List<MatchDto>>(matches);
                }

                return listWithMatches;

            }
        }
    }
}
