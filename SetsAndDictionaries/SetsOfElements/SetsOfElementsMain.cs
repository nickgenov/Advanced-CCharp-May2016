using System;
using System.Collections.Generic;
using System.Linq;

namespace SetsOfElements
{
    public class SetsOfElementsMain
    {
        public static void Main()
        {
            var setOne = new SortedSet<double>();
            var setTwo = new SortedSet<double>();
            var resultSet = new HashSet<double>();

            string input = Console.ReadLine();

            int[] lengths = input.Split(new char[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            int setOneLength = lengths[0];
            int setTwoLength = lengths[1];


            for (int i = 0; i < setOneLength; i++)
            {
                input = Console.ReadLine();
                double number = double.Parse(input);

                setOne.Add(number);
            }

            for (int i = 0; i < setTwoLength; i++)
            {
                input = Console.ReadLine();
                double number = double.Parse(input);

                setTwo.Add(number);
            }


            foreach (var number in setOne)
            {
                if (setTwo.Contains(number))
                {
                    resultSet.Add(number);
                }
            }

            string result = string.Join(" ", resultSet);
            Console.WriteLine(result);
        }
    }
}