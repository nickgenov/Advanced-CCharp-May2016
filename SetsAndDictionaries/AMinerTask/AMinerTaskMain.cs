using System;
using System.Collections.Generic;

namespace AMinerTask
{
    public class AMinerTaskMain
    {
        public static void Main()
        {
            var resources = new Dictionary<string, double>();

            int odd = 1;
            string input = Console.ReadLine();

            string lastKey = string.Empty;
            while (input != "stop")
            {
                if (odd == 1)
                {
                    lastKey = input;

                    if (resources.ContainsKey(lastKey) == false)
                    {
                        resources[lastKey] = 0;
                    }
                }
                else
                {
                    resources[lastKey] += double.Parse(input);
                }

                input = Console.ReadLine();
                odd *= -1;
            }

            //TODO FIX SOLUTION 75/100

            foreach (var resource in resources)
            {
                string result = string.Format("{0} -> {1}", resource.Key, resource.Value);
                Console.WriteLine(result);
            }
        }
    }
}