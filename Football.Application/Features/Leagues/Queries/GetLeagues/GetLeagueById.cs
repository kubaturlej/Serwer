using AutoMapper;
using Football.Application.Contracts.Persistence;
using Football.Application.Features.Leagues.Queries.GetPlayers;
using MediatR;
using System.Collections.Generic;
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
            private readonly IPlayerRepository _playerRepository;

            public Handler(ILeagueRepository repository, IMapper mapper, IPlayerRepository playerRepository)
            {
                _repository = repository;
                _mapper = mapper;
                _playerRepository = playerRepository;
            }
            public async Task<LeagueDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var leagues = await _repository.GetLeagueById(request.Id);

                var listWithMatches = _mapper.Map<LeagueDto>(leagues);

                var rounds = await _repository.GetScheduleByLeague(request.Id);
                var scorers = await _playerRepository.GetBestScorersForLeague(request.Id);

                listWithMatches.Scorers = _mapper.Map<List<PlayerDto>>(scorers);
                listWithMatches.Rounds =  _mapper.Map<List<RoundDto>>(rounds);

                return listWithMatches;
            }
        }
    }
}
