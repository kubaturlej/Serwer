using AutoMapper;
using Football.Application.Contracts.Persistence;
using Football.Application.Features.Leagues.Queries.GetLeagues;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Football.Application.Features.Teams.Queries.GetTeams
{
    public class GetTeamMatchForSpecificDay
    {
        public class Query : IRequest<List<MatchDto>>
        {
            public string Name { get; set; }
            public string Date { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<MatchDto>>
        {
            private readonly ITeamsRepository _repository;
            private readonly IMapper _mapper;

            public Handler(ITeamsRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<List<MatchDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var matches = await _repository.GetTeamMatchForSpecificDay(request.Name, request.Date);

                return _mapper.Map<List<MatchDto>>(matches);
            }
        }
    }
}
