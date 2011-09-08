using System;
using System.Collections.Generic;
using System.Linq;
using TaskDash.Core.Models.Tasks;

namespace TaskDash.Core.ExtensionMethods
{
    public static class Lists
    {
        public static bool CloselyMatches(this IEnumerable<IRankable> items, string matchPhrase)
        {
            return items != null 
                && items.Any(rankable => rankable.CloselyMatches(matchPhrase));
        }

        public static double MatchRanking(this IEnumerable<IRankable> items, string matchPhrase)
        {
            if (items == null)
                return 0;

            return items.Sum(rankable => rankable.MatchRanking(matchPhrase));
        }


        public static void CheckToCreateToday(this List<Log> logs)
        {
            if (logs == null)
            {
                return;
            }

            foreach (Log log in logs)
            {
                if (log.EntryDate.Date == DateTime.Today.Date)
                    return;
            }

            Log todaysLog = new Log();
            logs.Add(todaysLog);
        }
    }
}
