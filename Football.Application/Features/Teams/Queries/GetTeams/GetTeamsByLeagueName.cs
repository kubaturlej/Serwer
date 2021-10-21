using AutoMapper;
using Football.Application.Contracts.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Football.Application.Features.Leagues.Queries.GetTeams
{
    public class GetTeamsByLeagueName
    {
        public class Query : IRequest<List<TeamDto>>
        {
            public int Id { get; set; }
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
                var player = await _repository.GetTeamsByLeagueName(request.Id);

                return _mapper.Map<List<TeamDto>>(player);
            }
        }
    }
}
