using AutoMapper;
using Football.Application.Contracts.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Football.Application.Features.Leagues.Queries.GetPlayers
{
    public class GetPlayersByTeamName
    {
        public class Query : IRequest<List<PlayerDto>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<PlayerDto>>
        {
            private readonly IPlayerRepository _repository;
            private readonly IMapper _mapper;

            public Handler(IPlayerRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<List<PlayerDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var player = await _repository.GetPlayersByTeamName(request.Id);

                return _mapper.Map<List<PlayerDto>>(player);
            }
        }
    }
}
