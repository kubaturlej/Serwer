using AutoMapper;
using Football.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Football.Application.Features.Leagues.Queries.GetLeagues
{
    public class GetScheduleByDate
    {
        public class Query : IRequest<List<MatchDto>>
        {
            public int Id { get; set; }
            public string Date { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<MatchDto>>
        {
            private readonly ILeagueRepository _repository;
            private readonly IMapper _mapper;

            public Handler(ILeagueRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }
            public async Task<List<MatchDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var leagues = await _repository.GetScheduleByDate(request.Id, request.Date);

                return _mapper.Map<List<MatchDto>>(leagues);
            }
        }
    }
}
