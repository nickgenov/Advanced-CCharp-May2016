using System;
using System.Collections.Generic;

namespace Phonebook
{
    public class PhonebookMain
    {
        public static void Main()
        {
            var phonebook = new Dictionary<string, string>();

            string input = Console.ReadLine();

            while (input != "search")
            {
                string[] phoneData = input.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries);

                string personName = phoneData[0];
                string phoneNumber = phoneData[1];
                phonebook[personName] = phoneNumber;

                input = Console.ReadLine();
            }

            input = Console.ReadLine();

            while (input != "stop")
            {
                if (phonebook.ContainsKey(input))
                {
                    string phoneNumber = phonebook[input];

                    string result = string.Format("{0} -> {1}", input, phoneNumber);
                    Console.WriteLine(result);
                }
                else
                {
                    Console.WriteLine("Contact {0} does not exist.", input);
                }

                input = Console.ReadLine();
            }
        }
    }
}