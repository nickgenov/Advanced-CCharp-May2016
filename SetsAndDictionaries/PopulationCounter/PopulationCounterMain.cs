using System;
using System.Collections.Generic;
using System.Linq;

namespace PopulationCounter
{
    public class PopulationCounterMain
    {
        public static void Main()
        {
            var populationData = new Dictionary<string, Dictionary<string, long>>();

            string input = Console.ReadLine();
            while (input != "report")
            {
                string[] data = input.Split('|');

                string city = data[0];
                string country = data[1];
                long population = long.Parse(data[2]);

                if (populationData.ContainsKey(country) )
                {
                    populationData[country].Add(city, population);
                }
                else
                {
                    populationData.Add(country, new Dictionary<string, long>());
                    populationData[country].Add(city, population);
                }

                input = Console.ReadLine();
            }

            PrintOutput(populationData);
        }

        private static void PrintOutput(Dictionary<string, Dictionary<string, long>> populationData)
        {
            var sortedData = populationData.OrderByDescending(c => c.Value.Values.Sum());

            foreach (var country in sortedData)
            {
                Console.WriteLine("{0} (total population: {1})", country.Key, country.Value.Values.Sum());

                var cityData = country.Value.OrderByDescending(s => s.Value);

                foreach (var city in cityData)
                {
                    Console.WriteLine("=>{0}: {1}", city.Key, city.Value);
                }
            }
        }
    }
}