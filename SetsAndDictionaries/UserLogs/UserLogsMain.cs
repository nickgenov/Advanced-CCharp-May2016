using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UserLogs
{
    public class UserLogsMain
    {
        public static void Main()
        {
            var userLog = new SortedDictionary<string, Dictionary<string, int>>();

            string pattern = @"IP=([\S]+) message=([\S]+) user=([\S]+)";
            Regex regex = new Regex(pattern);

            string input = Console.ReadLine();
            while (input != "end")
            {
                Match match = regex.Match(input);
                string ipAddress = match.Groups[1].Value;
                string username = match.Groups[3].Value;


                if (userLog.ContainsKey(username) == false)
                {
                    userLog[username] = new Dictionary<string, int>();
                }
           
                var messageData = userLog[username];
                if (messageData.ContainsKey(ipAddress))
                {
                    messageData[ipAddress] += 1;
                }
                else
                {
                    messageData[ipAddress] = 1;
                }


                input = Console.ReadLine();
            }

            foreach (var entry in userLog)
            {
                string username = entry.Key;
                var messageCounts = new List<string>();

                foreach (var pair in entry.Value)
                {
                    string ipAddress = pair.Key;
                    int count = pair.Value;

                    string messageInfo = string.Format("{0} => {1}", ipAddress, count);
                    messageCounts.Add(messageInfo);
                }

                string result = string.Format("{0}:{1}{2}.",username, Environment.NewLine, string.Join(", ", messageCounts));
                Console.WriteLine(result);
            }
        }
    }
}