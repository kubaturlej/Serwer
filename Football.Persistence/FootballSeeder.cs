using AutoMapper;
using DataScraper;
using DataScraper.Scrapers;
using Football.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Football.Persistence
{
    public class FootballSeeder
    {
        public static async Task Seed(FootballDbContext dbContext, IMapper mapper)
        {
            if (!dbContext.Leagues.Any() && !dbContext.Teams.Any() && !dbContext.Players.Any())
            {
                var leaguesList = Common.leagues.ToList();
                var countries = Common.countries.ToList();
                var schedules = Common.schedules.ToList();
                var players = Common.players.ToList();

                var leagues = new List<League>();

                for (int i = 0; i < leaguesList.Count; i++)
                {
                    var tis = new TeamInfoScraper(leaguesList[i], countries[i]);
                    var teamsInfo = tis.GetTeams();
                    var mappedTeam = mapper.Map<IEnumerable<Team>>(teamsInfo);

                    var ps = new PlayerScraper(players[i]);
                    var playersList = ps.GetPlayers();

                    int counter = 0;

                    foreach (var playersInTeam in playersList)
                    {
                        var mappedPlayersList = mapper.Map<IEnumerable<Player>>(playersInTeam.Players);
                        mappedTeam.ToList()[counter].Players = mappedPlayersList.ToList();
                        counter++;
                    }

                    var ls = new LeagueScraper(leaguesList[i], countries[i]);
                    var league = ls.GetLeague();
                    var mappedLeague = mapper.Map<League>(league);

                    var ss = new ScheduleScraper(schedules[i]);
                    var schedule = ss.GetSchedule();
                    var mappedShedule = mapper.Map<IEnumerable<Round>>(schedule);

                    mappedLeague.Teams = mappedTeam.ToList();
                    mappedLeague.Rounds = mappedShedule.ToList();
                    leagues.Add(mappedLeague);
                }
                
                await dbContext.Leagues.AddRangeAsync(leagues);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                dbContext.SaveChanges();
            }
        }

        private static IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };

            return roles;
        }
    }
}
