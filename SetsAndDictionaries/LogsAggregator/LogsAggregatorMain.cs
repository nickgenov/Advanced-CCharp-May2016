using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LogsAggregator
{
    public class LogsAggregatorMain
    {
        public static void Main()
        {
            var logsAggregator = new SortedDictionary<string, SortedDictionary<string, int>>();

            string pattern = @"([0-9.]+) ([A-Za-z]+) ([0-9]+)";
            Regex regex = new Regex(pattern);

            string input = Console.ReadLine();
            int logsCount = int.Parse(input);

            for (int i = 0; i < logsCount; i++)
            {
                input = Console.ReadLine();

                Match match = regex.Match(input);
                if (match.Success == false)
                {
                    continue;
                }

                string ipAddress = match.Groups[1].Value;
                string user = match.Groups[2].Value;
                int duration = int.Parse(match.Groups[3].Value);

                if (LogDataIsValid(user, ipAddress, duration))
                {
                    StoreLogs(logsAggregator, user, ipAddress, duration);
                }
            }

            PrintOutput(logsAggregator);
        }

        private static void PrintOutput(SortedDictionary<string, SortedDictionary<string, int>> logsAggregator)
        {
            foreach (var entry in logsAggregator)
            {
                string user = entry.Key;
                int duration = 0;
                var ipAddresses = new List<string>();

                foreach (var ip in entry.Value)
                {
                    duration += ip.Value;
                    ipAddresses.Add(ip.Key);
                }

                string output = string.Format("{0}: {1} [{2}]", user, duration, string.Join(", ", ipAddresses));
                Console.WriteLine(output);
            }
        }

        private static void StoreLogs(SortedDictionary<string, SortedDictionary<string, int>> logsAggregator, string user, string ipAddress, int duration)
        {
            if (logsAggregator.ContainsKey(user))
            {
                if (logsAggregator[user].ContainsKey(ipAddress))
                {
                    logsAggregator[user][ipAddress] += duration;
                }
                else
                {
                    logsAggregator[user].Add(ipAddress, duration);
                }
            }
            else
            {
                logsAggregator.Add(user, new SortedDictionary<string, int>());
                logsAggregator[user].Add(ipAddress, duration);
            }
        }

        private static bool LogDataIsValid(string user, string ipAddress, int duration)
        {
            if (user.Length > 20)
            {
                return false;
            }
            if (duration > 1000)
            {
                return false;
            }
            if (ValidIp(ipAddress) == false)
            {
                return false;
            }

            return true;
        }

        private static bool ValidIp(string ipAddress)
        {
            string pattern = @"([0-9]+).([0-9]+).([0-9]+).([0-9]+)";
            Regex regex = new Regex(pattern);

            Match match = regex.Match(ipAddress);
            if (match.Success == false)
            {
                return false;
            }

            int a = int.Parse(match.Groups[1].Value);
            int b = int.Parse(match.Groups[2].Value);
            int c = int.Parse(match.Groups[3].Value);
            int d = int.Parse(match.Groups[4].Value);

            if (a > 255)
            {
                return false;
            }
            if (b > 255)
            {
                return false;
            }
            if (c > 255)
            {
                return false;
            }
            if (d > 255)
            {
                return false;
            }

            return true;
        }
    }
}