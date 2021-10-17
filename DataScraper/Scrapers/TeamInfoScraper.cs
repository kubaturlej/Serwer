using DataScraper.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataScraper.Scrapers
{
    public class TeamInfoScraper
    {
        private readonly string _league;
        private readonly string _country;
        private const string BaseUrl = "https://footystats.org/";

        public TeamInfoScraper(string league, string country)
        {
            _league = league;
            _country = country;
        }

        public IEnumerable<Table> GetTeams()
        {
            Console.WriteLine("Team scraper started ...");
            var web = new HtmlWeb();
            var document = web.Load(BaseUrl + _country + "/" + _league);

            var table = document.QuerySelectorAll("table.full-league-table")[0];

            var trs = table.QuerySelectorAll("tr").Skip(1);

            foreach (var tr in trs)
            {
                var tds = tr.QuerySelectorAll("td");


                var teamUrl = tds[2].QuerySelector("a").Attributes["href"].Value.TrimStart('/');

                var innerDocument = web.Load(BaseUrl + teamUrl);

                var innerTable = innerDocument.QuerySelector("table");

                var innterTrs = innerTable.QuerySelectorAll("tr").Skip(1).ToList();
                var goalsScoredPerMatch = innterTrs[2].QuerySelectorAll("td")[1].InnerText;
                var goalsConcededPerMacth = innterTrs[3].QuerySelectorAll("td")[1].InnerText;
                var avgPossession = innterTrs[7].QuerySelectorAll("td")[1].InnerText;

                var matchHistory = tds.QuerySelectorAll("li");

                var history = new List<string>();

                foreach (var match in matchHistory)
                {
                    history.Add(match.InnerText);
                }

                var standing = tds[0].InnerText.Trim();
                var teamName = tds[2].InnerText.Trim();
                var matches = tds[3].InnerText.Trim();
                var wins = tds[4].InnerText.Trim();
                var draws = tds[5].InnerText.Trim();
                var losses = tds[6].InnerText.Trim();
                var goalsScored = tds[7].InnerText.Trim();
                var goalConceded = tds[8].InnerText.Trim();
                var goals = tds[9].InnerText.Trim();
                var points = tds[10].InnerText.Trim();
                var cleanSheets = tds[13].InnerText.Trim();
                var avgGoals = tds[20].InnerText.Trim();

                yield return new Table
                {
                    TeamName = teamName,
                    TeamNationality = _country,
                    Standing = standing,
                    Matches = matches,
                    Wins = wins,
                    Draws = draws,
                    Losses = losses,
                    GoalScored = goalsScored,
                    GoalConceded = goalConceded,
                    GoalBalance = goals,
                    Points = points,
                    MacthesHistory = history,
                    CleanSheets = cleanSheets,
                    AvgGoalsPerMacth = avgGoals,
                    GoalsConcededPerMacth = goalsConcededPerMacth,
                    GoalsScoredPerMatch = goalsScoredPerMatch,
                    AvgPossession = avgPossession
                };
            }
        }
    }
}
