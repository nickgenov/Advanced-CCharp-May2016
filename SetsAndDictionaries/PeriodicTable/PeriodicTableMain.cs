using System;
using System.Collections.Generic;

namespace PeriodicTable
{
    public class PeriodicTableMain
    {
        public static void Main()
        {
            var uniqueCompounds = new SortedSet<string>();

            string input = Console.ReadLine();
            int number = int.Parse(input);


            for (int i = 0; i < number; i++)
            {
                input = Console.ReadLine();

                string[] compounds = input.Split(new char[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);

                foreach (var compound in compounds)
                {
                    uniqueCompounds.Add(compound);
                }
            }

            string result = string.Join(" ", uniqueCompounds);
            Console.WriteLine(result);
        }
    }
}