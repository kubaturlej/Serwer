using AutoMapper;
using Football.Application.Contracts.Persistence;
using Football.Application.Features.Leagues.Queries.GetLeagues;
using MediatR;
using System.Collections.Generic;
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
                var matches = await _repository.GetTeamSchedule(team.TeamName);
                var result = _mapper.Map<TeamDto>(team);
                result.Schedule = _mapper.Map<List<MatchDto>>(matches);
                return result;
            }
        }
    }
}
