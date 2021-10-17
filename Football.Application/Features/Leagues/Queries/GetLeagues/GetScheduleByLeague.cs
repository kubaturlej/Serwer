using AutoMapper;
using Football.Application.Contracts.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Football.Application.Features.Leagues.Queries.GetLeagues
{
    public class GetScheduleByLeague
    {
        public class Query : IRequest<List<RoundDto>>
        {
            public int Id{ get; set; }
        }

        public class Handler : IRequestHandler<Query, List<RoundDto>>
        {
            private readonly ILeagueRepository _repository;
            private readonly IMapper _mapper;

            public Handler(ILeagueRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }
            public async Task<List<RoundDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var leagues = await _repository.GetScheduleByLeague(request.Id);

                return _mapper.Map<List<RoundDto>>(leagues);
            }
        }
    }
}
