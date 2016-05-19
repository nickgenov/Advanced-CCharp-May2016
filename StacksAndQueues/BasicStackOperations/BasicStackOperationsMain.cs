using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicStackOperations
{
    public class BasicStackOperationsMain
    {
        public static void Main()
        {
            Stack<int> elementsStack = new Stack<int>();

            string input = Console.ReadLine();
            int[] commands = input.Split(' ').Select(int.Parse).ToArray();

            int elementsToPushCount = commands[0];
            int elementsToPopCount = commands[1];
            int elementToCheck = commands[2];

            input = Console.ReadLine();
            int[] integers = input.Split(' ').Select(int.Parse).ToArray();

            //push elements onto the stack
            for (int i = 0; i < elementsToPushCount; i++)
            {
                int element = integers[i];
                elementsStack.Push(element);
            }

            //pop elements from the top of the stack
            for (int i = 0; i < elementsToPopCount; i++)
            {
                elementsStack.Pop();
            }


            //check for an element in the stack

            if (elementsStack.Contains(elementToCheck))
            {
                Console.WriteLine("true");
            }
            else if(elementsStack.Count > 0)
            {
                Console.WriteLine(elementsStack.Min());
            }
            else
            {
                Console.WriteLine(0);
            }
        }
    }
}