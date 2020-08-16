using CryptoHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your Phrase to Encryt");
            var phrase = Console.ReadLine();

            Console.WriteLine("The Encrypted Phrase is: ");

            var encryptPhrase = Crypto.Encrypt(phrase, "MyKey");

            Console.WriteLine(encryptPhrase);

            Console.WriteLine("Enter \n Yes to Decrypt \n no to close");

            Regex rx = new Regex(@"\b(yes)\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if (rx.Matches(Console.ReadLine()).Count >= 1)
            {
                Console.WriteLine(Crypto.Decrypt(encryptPhrase, "MyKey"));
            }
            else
            {
                Console.WriteLine("Ok, Goodbye");
            }

            Console.WriteLine("Press Enter to quit");

            Console.ReadKey();
        }
    }
}
