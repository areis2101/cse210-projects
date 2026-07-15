using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace JournalApp
{
    public class Entry
    {
        public string Date { get; set; }
        public string Prompt { get; set; }
        public string Response { get; set; }

        public Entry() { }

        public Entry(string prompt, string response)
        {
            Date = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
            Prompt = prompt;
            Response = response;
        }

        public void Display()
        {
            Console.WriteLine($"--------------------------------------------------");
            Console.WriteLine($"Date: {Date}");
            Console.WriteLine($"Prompt: {Prompt}");
            Console.WriteLine($"Response: {Response}");
            Console.WriteLine($"--------------------------------------------------");
        }
    }

    public class Journal
    {
        private List<Entry> _entries = new List<Entry>();

        private List<string> _prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I could do one thing over today, what would it be?",
            "What is something new that I learned today?",
            "What am I most grateful for today?"
        };

        public string GetRandomPrompt()
        {
            Random random = new Random();
            int index = random.Next(_prompts.Count);
            return _prompts[index];
        }

        public void AddEntry(Entry newEntry)
        {
            _entries.Add(newEntry);
            Console.WriteLine("\nEntry successfully saved to memory!");
        }

        public void DisplayAll()
        {
            if (_entries.Count == 0)
            {
                Console.WriteLine("\nThe journal is empty.");
                return;
            }

            Console.WriteLine("\n=== ALL JOURNAL ENTRIES ===");
            foreach (var entry in _entries)
            {
                entry.Display();
            }
        }

        public void SaveToFile(string filename)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(_entries, options);

                File.WriteAllText(filename, jsonString);
                Console.WriteLine($"\nJournal successfully saved to file: {filename}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError saving file: {ex.Message}");
            }
        }

        public void LoadFromFile(string filename)
        {
            try
            {
                if (!File.Exists(filename))
                {
                    Console.WriteLine("\nError: The specified file does not exist.");
                    return;
                }

                string jsonString = File.ReadAllText(filename);
                _entries = JsonSerializer.Deserialize<List<Entry>>(jsonString);

                Console.WriteLine($"\nJournal successfully loaded! {_entries.Count} entries recovered.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError loading file: {ex.Message}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Journal myJournal = new Journal();
            string choice = "";

            Console.WriteLine("Welcome to your C# Digital Journal!");

            while (choice != "5")
            {
                Console.WriteLine("\nPlease choose one of the following options:");
                Console.WriteLine("1. Write a new entry");
                Console.WriteLine("2. Display the journal");
                Console.WriteLine("3. Load the journal from a file");
                Console.WriteLine("4. Save the journal to a file");
                Console.WriteLine("5. Quit");
                Console.Write("What would you like to do? ");

                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        string prompt = myJournal.GetRandomPrompt();
                        Console.WriteLine($"\nPrompt: {prompt}");
                        Console.Write("> ");
                        string response = Console.ReadLine();

                        Entry newEntry = new Entry(prompt, response);
                        myJournal.AddEntry(newEntry);
                        break;

                    case "2":
                        myJournal.DisplayAll();
                        break;

                    case "3":
                        Console.Write("\nEnter filename to load (e.g., journal.json): ");
                        string loadFilename = Console.ReadLine();
                        myJournal.LoadFromFile(loadFilename);
                        break;

                    case "4":
                        Console.Write("\nEnter filename to save (e.g., journal.json): ");
                        string saveFilename = Console.ReadLine();
                        myJournal.SaveToFile(saveFilename);
                        break;

                    case "5":
                        Console.WriteLine("\nThank you for using the journal. Goodbye!");
                        break;

                    default:
                        Console.WriteLine("\nInvalid option. Please try again.");
                        break;
                }
            }
        }
    }
}
