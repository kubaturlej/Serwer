using AutoMapper;
using Football.Application.Features.Leagues.Queries.GetLeagues;
using Football.Application.Features.Leagues.Queries.GetPlayers;
using Football.Application.Features.Leagues.Queries.GetTeams;

namespace Football.Application.Mapping
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<DataScraper.Models.League, Domain.Entities.League>();
            CreateMap<DataScraper.Models.Table, Domain.Entities.Team>()
                .ForMember(m => m.MacthesHistory, c => c.MapFrom(s => string.Join(",", s.MacthesHistory)));

            CreateMap<DataScraper.Models.Round, Domain.Entities.Round>();
            CreateMap<DataScraper.Models.Match, Domain.Entities.Match>();
            CreateMap<DataScraper.Models.Player, Domain.Entities.Player>();
            CreateMap<Domain.Entities.League, LeagueDto>();
            CreateMap<Domain.Entities.Player, PlayerDto>();
            CreateMap<Domain.Entities.Team, TeamDto>();
            CreateMap<Domain.Entities.Round, RoundDto>();
            CreateMap<Domain.Entities.Match, MatchDto>();
    
        }
    }
}
