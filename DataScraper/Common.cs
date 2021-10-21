using System.Collections.Generic;

namespace DataScraper
{
    public class Common
    {
        public static readonly List<string> leagues = new List<string> { "premier-league", "bundesliga", "serie-a", "la-liga", "ligue-1", "ekstraklasa" };
        public static readonly List<string> countries = new List<string> { "england", "germany", "italy", "spain", "france", "poland" };
        public static readonly List<string> schedules = new List<string> { "all_matches/eng-premier-league-2021-2022/", "all_matches/bundesliga-2021-2022/", "all_matches/ita-serie-a-2021-2022/", "all_matches/esp-primera-division-2021-2022/", "all_matches/fra-ligue-1-2021-2022/", "all_matches/pol-ekstraklasa-2021-2022/" };
        public static readonly List<string> players = new List<string> { "en/comps/9/Premier-League-Stats", "en/comps/20/Bundesliga-Stats", "en/comps/11/Serie-A-Stats", "en/comps/12/La-Liga-Stats", "en/comps/13/Ligue-1-Stats", "en/comps/36/Ekstraklasa-Stats" };
    }
}
