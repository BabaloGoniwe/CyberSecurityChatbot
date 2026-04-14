using System;
using System.Threading;
using CyberSecurityChatbot.Models;
using CyberSecurityChatbot.Services;

class Program
{
    static void Main()
    {
        // Single-instance guard to prevent multiple running copies that lock the exe.
        const string mutexName = "Local\\CyberSecurityChatbot_SingleInstance_Mutex";
        bool createdNew;

        using (var mutex = new Mutex(initiallyOwned: true, name: mutexName, createdNew: out createdNew))
        {
            if (!createdNew)
            {
                Console.WriteLine("Another instance is already running. Exiting.");
                return;
            }

            // Allow graceful exit via Ctrl+C
            bool exitRequested = false;
            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true; // prevent abrupt termination so we can clean up
                exitRequested = true;
                Console.WriteLine("\nExit requested. Shutting down...");
            };

            try
            {
                AudioPlayer audio = new AudioPlayer();
                UIHelper ui = new UIHelper();
                Chatbot bot = new Chatbot();

                // Play audio (non-blocking)
                audio.PlayGreeting();

                // Show ASCII header
                ui.ShowHeader();

                // Get user name
                Console.Write("Enter your name: ");
                string name = Console.ReadLine() ?? string.Empty;

                while (string.IsNullOrWhiteSpace(name) && !exitRequested)
                {
                    Console.Write("Name cannot be empty. Enter your name: ");
                    name = Console.ReadLine() ?? string.Empty;
                }

                if (exitRequested) return;

                User user = new User { Name = name };

                ui.TypeText($"\nWelcome, {user.Name}! Let's learn about cybersecurity.\n");
                ui.TypeText("Type 'exit' or 'quit' to close the program.\n");

                // Chat loop
                while (!exitRequested)
                {
                    Console.Write("You: ");
                    string input = Console.ReadLine() ?? string.Empty;

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine("⚠️ Please enter something.");
                        continue;
                    }

                    string lower = input.Trim().ToLowerInvariant();
                    if (lower == "exit" || lower == "quit")
                    {
                        Console.WriteLine("Goodbye!");
                        break;
                    }

                    string response = bot.GetResponse(input, user.Name);
                    Console.WriteLine("Bot: " + response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error: " + ex.Message);
            }
        } // mutex released here
    }
}