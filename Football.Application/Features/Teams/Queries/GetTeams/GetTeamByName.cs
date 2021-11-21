using AutoMapper;
using Football.Application.Contracts.Persistence;
using Football.Application.Features.Leagues.Queries.GetTeams;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Football.Application.Features.Teams.Queries.GetTeams
{
    public class GetTeamByName
    {
        public class Query : IRequest<List<TeamDto>>
        {
            public string Name { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<TeamDto>>
        {
            private readonly ITeamsRepository _repository;
            private readonly IMapper _mapper;

            public Handler(ITeamsRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<List<TeamDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var teams = await _repository.GetTeamsByName(request.Name);

                return _mapper.Map<List<TeamDto>>(teams);
            }
        }
    }
}
