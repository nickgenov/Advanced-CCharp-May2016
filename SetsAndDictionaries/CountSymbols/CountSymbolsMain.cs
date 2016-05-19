using System;
using System.Collections.Generic;

namespace CountSymbols
{
    public class CountSymbolsMain
    {
        public static void Main()
        {
            var symbolCounts = new SortedDictionary<char, int>();

            string text = Console.ReadLine();

            foreach (char symbol in text)
            {
                if (symbolCounts.ContainsKey(symbol) == false)
                {
                    symbolCounts[symbol] = 1;

                }
                else
                {
                    symbolCounts[symbol] += 1;
                }
            }


            foreach (var pair in symbolCounts)
            {
                char key = pair.Key;
                int value = pair.Value;

                string result = string.Format("{0}: {1} time/s", key, value);

                Console.WriteLine(result);
            }
        }
    }
}