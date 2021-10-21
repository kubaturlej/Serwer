using AutoMapper;
using DataScraper.Models;
using Football.Application.Contracts.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Football.Application.Features.Leagues.Commands
{
    public class UpdateLeagues
    {
        public class Command : IRequest<Unit>
        {
            public List<League> Leagues { get; set; }
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
                await _repository.UpdateLegaueInfo(_mapper.Map<List<Domain.Entities.League>>(request.Leagues));

                return Unit.Value;
            }
        }
    }
}
