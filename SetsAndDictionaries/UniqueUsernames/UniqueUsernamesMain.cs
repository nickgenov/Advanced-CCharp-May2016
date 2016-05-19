using System;
using System.Collections.Generic;

namespace UniqueUsernames
{
    public class UniqueUsernamesMain
    {
        public static void Main()
        {
            var names = new HashSet<string>();

            string input = Console.ReadLine();
            int count = int.Parse(input);

            for (int i = 0; i < count; i++)
            {
                input = Console.ReadLine();
                names.Add(input);
            }

            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
        }
    }
}