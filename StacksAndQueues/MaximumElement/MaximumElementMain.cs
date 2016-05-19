using System;
using System.Collections.Generic;
using System.Linq;

namespace MaximumElement
{
    public class MaximumElementMain
    {
        public static void Main()
        {
            Stack<int> stackOfElements = new Stack<int>();

            string input = Console.ReadLine();
            int numberOfQueries = int.Parse(input);

            for (int i = 0; i < numberOfQueries; i++)
            {
                input = Console.ReadLine();
                int[] elements = QueryToArray(input);

                int command = elements[0];

                if (command == 1)
                {
                    int element = elements[1];
                    stackOfElements.Push(element);
                }
                else if (command == 2)
                {
                    stackOfElements.Pop();
                }
                else if (command == 3)
                {
                    Console.WriteLine(stackOfElements.Max());
                }
            }
        }

        private static int[] QueryToArray(string input)
        {
            string[] stringArray = input.Split(' ');
            int[] result = new int[stringArray.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = int.Parse(stringArray[i]);
            }

            return result;
        }
    }
}