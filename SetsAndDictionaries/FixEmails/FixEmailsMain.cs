using System;
using System.Collections.Generic;

namespace FixEmails
{
    public class FixEmailsMain
    {
        public static void Main()
        {
            var personEmails = new Dictionary<string, string>();

            int odd = 1;
            string lastKey = string.Empty;

            string input = Console.ReadLine();
            while (input != "stop")
            {
                if (odd == 1)
                {
                    if (personEmails.ContainsKey(input) == false)
                    {
                        personEmails[input] = string.Empty;
                    }

                    lastKey = input;
                }
                else
                {
                    personEmails[lastKey] = input;
                }

                input = Console.ReadLine();
                odd *= -1;
            }

            foreach (var personEmail in personEmails)
            {
                if (EmailIsValid(personEmail.Value))
                {
                    Console.WriteLine("{0} -> {1}", personEmail.Key, personEmail.Value);
                }
            }
        }

        private static bool EmailIsValid(string email)
        {
            if (email.ToLower().EndsWith("us"))
            {
                return false;
            }
            if (email.ToLower().EndsWith("uk"))
            {
                return false;
            }

            return true;
        }
    }
}