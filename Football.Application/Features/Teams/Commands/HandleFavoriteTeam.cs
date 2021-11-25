using AutoMapper;
using Football.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Football.Application.Features.Teams.Commands
{
    public class HandleFavoriteTeam
    {
        public class Command : IRequest<Unit>
        {
            public int UserId { get; set; }
            public int TeamId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly ITeamsRepository _repository;
            private readonly IMapper _mapper;

            public Handler(ITeamsRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _repository.HandleFavoriteTeam(request.UserId, request.TeamId);

                return Unit.Value;
            }

        }
    }
}
