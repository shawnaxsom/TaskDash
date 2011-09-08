using System;
using System.Text.RegularExpressions;


namespace TaskDash.ExtensionMethods
{
    public static class Strings
    {
        public static bool Contains(this string original, string value, StringComparison comparisionType)
        {
            return original.IndexOf(value, comparisionType) >= 0;
        }

        public static bool MatchesRegex(this string text, string regex)
        {
            if (text == null || regex == null)
                return false;

            Match match = Regex.Match(text, regex);
            return match.Success;
        }

        public static bool CloselyMatches(this string testedPhrase, string matchPhrase)
        {
            return MatchRanking(testedPhrase, matchPhrase) > 0;
        }

        public static double MatchRanking(this string testedPhrase, string matchPhrase)
        {
            double rank = 0;

            if (testedPhrase == null)
                return 0;

            if (testedPhrase.MatchesRegex(matchPhrase))
                rank += 100;

            bool containsAllWords = true;
            string[] words = matchPhrase.Split(' ');
            foreach (string word in words)
            {
                if (testedPhrase.Contains(word, StringComparison.CurrentCultureIgnoreCase))
                {
                    rank += 50;
                }
            }

            return rank;
        }
    }
}
