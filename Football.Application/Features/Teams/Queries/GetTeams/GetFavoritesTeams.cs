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
    public class GetFavoritesTeams
    {
        public class Query : IRequest<List<TeamDto>>
        {
            public int UserId { get; set; }
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
                var favoritesTeams = await _repository.GetFavoritesTeams(request.UserId);

                return _mapper.Map<List<TeamDto>>(favoritesTeams);
            }
        }
    }
}
