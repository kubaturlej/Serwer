using AutoMapper;
using Football.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Football.Application.Features.Leagues.Queries.GetTeams
{
    public class GetTeam
    {
        public class Query : IRequest<TeamDto>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, TeamDto>
        {
            private readonly ITeamsRepository _repository;
            private readonly IMapper _mapper;

            public Handler(ITeamsRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<TeamDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var team = await _repository.GetTeam(request.Id);

                return _mapper.Map<TeamDto>(team);
            }
        }
    }
}
