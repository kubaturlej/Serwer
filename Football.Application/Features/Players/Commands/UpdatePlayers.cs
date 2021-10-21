using AutoMapper;
using DataScraper.Models;
using Football.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Football.Application.Features.Players
{
    public class UpdatePlayers
    {
        public class Command : IRequest<Unit>
        {
            public List<Player> Players { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IPlayerRepository _repository;
            private readonly IMapper _mapper;

            public Handler(IPlayerRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _repository.UpdatePlayersInfo(_mapper.Map<List<Domain.Entities.Player>>(request.Players));

                return Unit.Value;
            }
        }
    }
}
