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
        List<DataScraper.Models.League> GetLeaguesInfoForUpdate();
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
            var leaguesList = Common.leagues;
            var countries = Common.countries;

            var result = new List<DataScraper.Models.Match>();

            for (int i = 0; i < leaguesList.Count; i++)
            {
                var ls = new LeagueScraper(leaguesList[i], countries[i]);
                var league = ls.GetLeague();
                result.Add(league);
            }

            return result;
        }

        public List<DataScraper.Models.Table> GetTeamsInfoForUpdate()
        {
            var leaguesList = Common.leagues;
            var countries = Common.countries;

            var teamsInfo = new List<DataScraper.Models.Table>();

            for (int i = 0; i < leaguesList.Count; i++)
            {
                var tis = new TeamInfoScraper(leaguesList[i], countries[i]);
                teamsInfo = tis.GetTeams().ToList();
            }

            return teamsInfo;
        }

        public List<DataScraper.Models.Player> GetPlayersInfoForUpdate()
        {
            var leaguesList = Common.leagues;
            var countries = Common.countries;

            var result = new List<DataScraper.Models.Player>();

            for (int i = 0; i < leaguesList.Count; i++)
            {
                var ls = new LeagueScraper(leaguesList[i], countries[i]);
                var league = ls.GetLeague();
                result.Add(league);
            }

            return result;
        }

    }
}
