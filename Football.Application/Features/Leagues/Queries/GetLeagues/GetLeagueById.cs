using AutoMapper;
using Football.Application.Contracts.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Football.Application.Features.Leagues.Queries.GetLeagues
{
    public class GetLeagueById
    {
        public class Query : IRequest<LeagueDto>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, LeagueDto>
        {
            private readonly ILeagueRepository _repository;
            private readonly IMapper _mapper;

            public Handler(ILeagueRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }
            public async Task<LeagueDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var leagues = await _repository.GetLeagueById(request.Id);

                return _mapper.Map<LeagueDto>(leagues);
            }
        }
    }
}
