using AutoMapper;
using DataScraper.Models;
using Football.Application.Contracts.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Football.Application.Features.Leagues.Commands
{
    public class UpdateMatches
    {
        public class Command : IRequest<Unit>
        {
            public List<Match> Matches { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly ILeagueRepository _repository;
            private readonly IMapper _mapper;

            public Handler(ILeagueRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _repository.UpdateMatchesInfo(_mapper.Map<List<Domain.Entities.Match>>(request.Matches));

                return Unit.Value;
            }
        }
    }
}
