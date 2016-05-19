using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SrabskoUnleashed
{
    public class SrabskoUnleashedMain
    {
        public static void Main()
        {
            var venueData = new Dictionary<string, Dictionary<string, long>>();

            string pattern = @"([a-zA-Z ]+) @([a-zA-Z ]+) ([0-9]+) ([0-9]+)";
            Regex regex = new Regex(pattern);

            string input = Console.ReadLine();
            while (input != "End")
            {
                Match match = regex.Match(input);
                if (match.Success == false)
                {
                    input = Console.ReadLine();
                    continue;
                }

                string singer = match.Groups[1].Value;
                string venue = match.Groups[2].Value;

                if (ValidWordCount(singer) && ValidWordCount(venue) == false)
                {
                    input = Console.ReadLine();
                    continue;
                }

                int ticketsPrice = int.Parse(match.Groups[3].Value);
                int ticketsCount = int.Parse(match.Groups[4].Value);
                long moneyEarned = ticketsPrice * ticketsCount;

                StoreVenueData(venueData, venue, singer, moneyEarned);

                input = Console.ReadLine();
            }

            PrintOutput(venueData);
        }

        private static void PrintOutput(Dictionary<string, Dictionary<string, long>> venueData)
        {
            foreach (var venue in venueData)
            {
                Console.WriteLine("{0}", venue.Key);

                var singerData = venue.Value.OrderByDescending(s => s.Value);
                foreach (var singer in singerData)
                {
                    Console.WriteLine("#  {0} -> {1}", singer.Key, singer.Value);
                }
            }
        }

        private static void StoreVenueData(Dictionary<string, Dictionary<string, long>> venueData, string venue, string singer, long moneyEarned)
        {
            if (venueData.ContainsKey(venue))
            {
                var singerData = venueData[venue];

                if (singerData.ContainsKey(singer))
                {
                    venueData[venue][singer] += moneyEarned;
                }
                else
                {
                    venueData[venue].Add(singer, moneyEarned);
                }
            }
            else
            {
                venueData[venue] = new Dictionary<string, long>()
                {
                    {singer, moneyEarned}
                };
            }
        }

        private static bool ValidWordCount(string text)
        {
            if (CountWords(text) < 1 || CountWords(text) > 3)
            {
                return false;
            }

            return true;
        }

        public static int CountWords(string text)
        {
            MatchCollection collection = Regex.Matches(text, @"[\S]+");
            return collection.Count;
        }
    }
}