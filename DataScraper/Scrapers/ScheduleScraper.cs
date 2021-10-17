using DataScraper.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace DataScraper.Scrapers
{
    public class ScheduleScraper
    {
        private readonly string _league;
        private const string BaseUrl = "https://www.worldfootball.net/";

        public ScheduleScraper(string league)
        {
            _league = league;
        }

        public IEnumerable<Round> GetSchedule()
        {
            Console.WriteLine("Schedule scraper started ...");
            var web = new HtmlWeb();
            var document = web.Load(BaseUrl + _league);

            var roundsList = new List<Round>();
            var matches = new List<Match>();
            var round = new Round();

            var table = document.QuerySelectorAll("table.standard_tabelle")[0];

            var trs = table.QuerySelectorAll("tr");


            if (_league == "all_matches/bundesliga-2021-2022/" || _league == "all_matches/pol-ekstraklasa-2021-2022/")
            {
                string data = string.Empty;
                string time = string.Empty;
                for (int i = 0; i < trs.Count; i++)
                {
                    if (i % 10 == 0)
                    {
                        matches.Clear();
                        round = new Round();
                        round.RoundNumber = trs[i].InnerText.Trim();
                    }
                    else
                    {
                        var tds = trs[i].QuerySelectorAll("td");

                        if (!string.IsNullOrEmpty(tds[0].InnerText))
                        {
                            data = tds[0].InnerText;
                        }

                        if (!string.IsNullOrEmpty(tds[1].InnerText))
                        {
                            time = tds[1].InnerText;
                        }

                        matches.Add(new Match { Date = data, Time = time, FirstTeam = tds[2].InnerText, SecondTeam = tds[4].InnerText, Score = tds[5].InnerText.Trim() });
                    }
                    if ((i + 1) % 10 == 0 && i > 0)
                    {
                        foreach (var match in matches)
                        {
                            round.Matches.Add(match);
                        }
                        roundsList.Add(round);
                    }
                }
            }
            else
            {
                string data = string.Empty;
                string time = string.Empty;
                for (int i = 0; i < trs.Count; i++)
                {
                    if (i % 11 == 0)
                    {
                        matches.Clear();
                        round = new Round();
                        round.RoundNumber = trs[i].InnerText.Trim();
                    }
                    else
                    {
                        var tds = trs[i].QuerySelectorAll("td");
  
                        if (!string.IsNullOrEmpty(tds[0].InnerText))
                        {
                            data = tds[0].InnerText;
                        }

                        if (!string.IsNullOrEmpty(tds[1].InnerText))
                        {
                            time = tds[1].InnerText;
                        }

                        matches.Add(new Match { Date = data, Time = time, FirstTeam = tds[2].InnerText, SecondTeam = tds[4].InnerText, Score = tds[5].InnerText.Trim() });
                    }
                    if ((i + 1) % 11 == 0 && i > 0)
                    {
                        foreach (var match in matches)
                        {
                            round.Matches.Add(match);
                        }
                        roundsList.Add(round);
                    }
                }
            }

            return roundsList;
        }
    }
}
