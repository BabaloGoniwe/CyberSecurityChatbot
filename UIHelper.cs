using System;
using System.Threading;

namespace CyberSecurityChatbot.Services
{
    public class UIHelper
    {
        public void ShowHeader()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(@"
   _____            _                ____        _   
  / ____|          | |              |  _ \      | |  
 | |     ___  _ __ | |_ ___  _ __   | |_) | ___ | |_ 
 | |    / _ \| '_ \| __/ _ \| '__|  |  _ < / _ \| __|
 | |___| (_) | | | | || (_) | |     | |_) | (_) | |_ 
  \_____\___/|_| |_|\__\___/|_|     |____/ \___/ \__|
");

            Console.ResetColor();
            Console.WriteLine("=== Cybersecurity Awareness Bot ===\n");
        }

        public void TypeText(string text)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(20);
            }
            Console.WriteLine();
        }
    }
}