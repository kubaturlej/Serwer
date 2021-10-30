using AutoMapper;
using Football.Application.Contracts.Persistence;
using Football.Application.Features.Leagues.Queries.GetPlayers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Football.Application.Features.Players.Queries.GetPlayers
{
    public class GetBestScorersForLeague
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
                var players = await _repository.GetBestScorersForLeague(request.Id);

                return _mapper.Map<List<PlayerDto>>(players);
            }
        }
    }
}
