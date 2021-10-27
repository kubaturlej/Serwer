using DataScraper.Models;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataScraper.Scrapers
{
    public class LeagueScraper
    {
        private readonly string _league;
        private readonly string _country;
        private readonly ILogger _logger;
        private const string BaseUrl = "https://footystats.org/";

        public LeagueScraper(string league, string country, ILogger logger)
        {
            _league = league;
            _country = country;
            _logger = logger;
        }

        public League GetLeague()
        {
            _logger.LogInformation($"League scraper started for {_league} ...");
            var web = new HtmlWeb();
            var document = web.Load(BaseUrl + _country + "/" + _league);

            var logo = document.QuerySelector("img.teamCrest").Attributes["src"].Value;

            var leagueName = document.QuerySelector(".teamName");

            var matchNumber = document.QuerySelectorAll(".w65 ")[3];

            var tokens = matchNumber.InnerText.Trim().Split('/');

            var matchesCompleted = tokens[0];
            var totalMatches = tokens[1].Split(' ')[0];

            double leagueProgress = Math.Floor((double.Parse(matchesCompleted) / int.Parse(totalMatches)) * 100);


            return new League
            {
                LeagueName = leagueName.InnerText.Replace("Stats", "").Trim(),
                LeagueProgress = $"{leagueProgress}%",
                TotalMatches = totalMatches,
                MatchesCompleted = matchesCompleted,
                Logo = logo
            };
        }
    }
}
