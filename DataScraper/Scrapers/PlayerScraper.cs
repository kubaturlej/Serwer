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
    public class PlayerScraper
    {
        private readonly string _league;
        private readonly ILogger _logger;
        private const string BaseUrl = "https://fbref.com/";

        public PlayerScraper(string league, ILogger logger)
        {
            _league = league;
            _logger = logger;
        }

        public IEnumerable<TeamPlayers> GetPlayers()
        {
            _logger.LogInformation($"League scraper started for {_league} ...");
            var web = new HtmlWeb();

            var document = web.Load(BaseUrl + _league);
            var table = document.QuerySelector(".stats_table");

            var trs = table.QuerySelectorAll("tr").Skip(1);


            var teamPlayersList = new List<TeamPlayers>();

            foreach (var tr in trs)
            {
                var tds = tr.QuerySelectorAll("td");
                var teamUrl = tds[0].QuerySelector("a").Attributes["href"].Value.TrimStart('/');
                var playersList = new List<Player>();
                var innerDocument = web.Load(BaseUrl + teamUrl);

                var innerTable = innerDocument.QuerySelector("table");
                var innerTrs = innerTable.QuerySelectorAll("tr").Skip(2);
                innerTrs = innerTrs.Take(innerTrs.Count() - 2);

                foreach (var innerTr in innerTrs)
                {
                    var th = innerTr.QuerySelector("th");
                    var innerTds = innerTr.QuerySelectorAll("td");

                    var nationality = innerTds[0].InnerText.Split(' ')[1];

                    playersList.Add(new Player
                    {
                        Name = th.InnerText,
                        Age = innerTds[2].InnerText,
                        Assists = innerTds[8].InnerText,
                        Goals = innerTds[7].InnerText,
                        MatchesPlayed = innerTds[3].InnerText,
                        MinutesPlayed = innerTds[5].InnerText,
                        Nationality = nationality,
                        Position = innerTds[1].InnerText,
                        RedCards = innerTds[13].InnerText,
                        StartedFromBegin = innerTds[4].InnerText,
                        YellowCards = innerTds[12].InnerText
                    });
                }
                teamPlayersList.Add(new TeamPlayers { Players = playersList });
            }
            return teamPlayersList;
        }
    }
}
