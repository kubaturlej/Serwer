using DataScraper;
using DataScraper.Scrapers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Football.API.Services
{
    public interface IUpdateDatabaseService
    {
        public List<DataScraper.Models.League> GetLeaguesInfoForUpdate();
        public List<DataScraper.Models.Match> GetMatchInfoForUpdate();
        public List<DataScraper.Models.Table> GetTeamsInfoForUpdate();
        public List<DataScraper.Models.Player> GetPlayersInfoForUpdate();
    }

    public class UpdateDatabaseService : IUpdateDatabaseService
    {
        public List<DataScraper.Models.League> GetLeaguesInfoForUpdate()
        {
            var leaguesList = Common.leagues;
            var countries = Common.countries;

            var result = new List<DataScraper.Models.League>();

            for (int i = 0; i < leaguesList.Count; i++)
            {
                var ls = new LeagueScraper(leaguesList[i], countries[i]);
                var league = ls.GetLeague();
                result.Add(league);
            }

            return result;
        }

        public List<DataScraper.Models.Match> GetMatchInfoForUpdate()
        {
            var schedules = Common.schedules;

            var result = new List<DataScraper.Models.Match>();

            for (int i = 0; i < schedules.Count; i++)
            {
                var ss = new ScheduleScraper(schedules[i]);
                var schedule = ss.GetSchedule();
                foreach (var rounds in schedule)
                {
                    foreach (var match in rounds.Matches)
                    {
                        result.Add(match);
                    }
                }
            }

            return result;
        }

        public List<DataScraper.Models.Table> GetTeamsInfoForUpdate()
        {
            var leaguesList = Common.leagues;
            var countries = Common.countries;

            var result = new List<DataScraper.Models.Table>();

            for (int i = 0; i < leaguesList.Count; i++)
            {
                var tis = new TeamInfoScraper(leaguesList[i], countries[i]);
                var teams = tis.GetTeams();
                foreach (var team in teams)
                {
                    result.Add(team);
                }
            }

            return result;
        }

        public List<DataScraper.Models.Player> GetPlayersInfoForUpdate()
        {
            var leaguesList = Common.leagues;
            var players = Common.players;

            var result = new List<DataScraper.Models.Player>();

            for (int i = 0; i < leaguesList.Count; i++)
            {
                var ps = new PlayerScraper(players[i]);
                var playersList = ps.GetPlayers();

                foreach (var teamPlayer in playersList)
                {
                    foreach (var player in teamPlayer.Players)
                    {
                        result.Add(player);
                    }
                }
            }

            return result;
        }

    }
}
