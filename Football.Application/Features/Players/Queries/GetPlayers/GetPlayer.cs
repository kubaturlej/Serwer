using AutoMapper;
using Football.Application.Contracts.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Football.Application.Features.Leagues.Queries.GetPlayers
{
    public class GetPlayer
    {
        public class Query : IRequest<PlayerDto>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, PlayerDto>
        {
            private readonly IPlayerRepository _repository;
            private readonly IMapper _mapper;


            public Handler(IPlayerRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<PlayerDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var player = await _repository.GetPlayer(request.Id);

                return _mapper.Map<PlayerDto>(player);
            }
        }
    }
}
