using System;
using System.Collections.Generic;
using System.Linq;

namespace ReverseNumbers
{
    public class ReverseNumbersMain
    {
        public static void Main()
        {
            var stackOfNumbers = new Stack<int>();
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                return;
            }

            //TODO fix problem in this line
            int[] numbers = input.Split(' ').Select(int.Parse).ToArray();

            foreach (var number in numbers)
            {
                stackOfNumbers.Push(number);
            }

            int stackCount = stackOfNumbers.Count;
            var result = new List<string>();

            for (int i = 0; i < stackCount; i++)
            {
                string number = stackOfNumbers.Pop().ToString();
                result.Add(number);
            }

            Console.WriteLine(string.Join(" ", result));
        }
    }
}