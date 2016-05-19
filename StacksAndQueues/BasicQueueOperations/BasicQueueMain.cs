using System;
using System.Collections;
using System.Linq;

namespace BasicQueueOperations
{
    public class BasicQueueMain
    {
        public static void Main()
        {
            var elementQueue = new Queue();

            string input = Console.ReadLine();
            string[] stringElements = input.Split(' ');
            int[] commands = stringElements.Select(int.Parse).ToArray();

            int elementsToEnqueue = commands[0];
            int elementsToDequeue = commands[1];
            int elementToCheck = commands[2];

            input = Console.ReadLine();
            stringElements = input.Split(' ');
            int[] elements = stringElements.Select(int.Parse).ToArray();

            for (int i = 0; i < elementsToEnqueue; i++)
            {
                int element = elements[i];
                elementQueue.Enqueue(element);
            }

            for (int i = 0; i < elementsToDequeue; i++)
            {
                elementQueue.Dequeue();
            }


            if (elementQueue.Count == 0)
            {
                Console.WriteLine(0);
            }
            else if (elementQueue.Contains(elementToCheck))
            {
                Console.WriteLine("true");
            }
            else
            {
                int minElement = (int)elementQueue.ToArray().Min();
                Console.WriteLine(minElement);
            }
        }
    }
}