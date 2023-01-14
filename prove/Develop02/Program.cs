using System;

class Program
{
    static void Main(string[] args)
    {
        // ! Functions 
        static int Menu()
        {
            Console.WriteLine(" 1: Write");
            Console.WriteLine(" 2: Display");
            Console.WriteLine(" 3: Load");
            Console.WriteLine(" 4: Save"); 
            Console.WriteLine(" 5: Schedule"); 
            Console.WriteLine(" 6: Quit");
            Console.WriteLine("");
            Console.Write("input > ");
            string string_answer = Console.ReadLine();
            Console.WriteLine("");
            int answer = int.Parse(string_answer);
            return answer;
        }

        // * Ive commented this out for now. 

        static string getPrompt(List<string> prompts)
        {   
            int index = new Random().Next(prompts.Count);
            string prompt = "";
            prompt = prompts[index];
            return prompt;
        }

        // ! Declaring variables 

        int answer = 5; 

        string prompt = "";

        DateTime date = DateTime.Today; 
        string dateString = date.ToString(); 
        int today = date.Day; 
        string day = today.ToString();
        string month = date.ToString("MMMM");




        // ! Code starts here 

        List<string> prompts = new List<string>();
        prompt = "Who was the most interesting person I interacted with today?";
        prompts.Add(prompt);
        prompt = "What was the best part of my day?";
        prompts.Add(prompt);
        prompt = "What was the strongest emotion I felt today?";
        prompts.Add(prompt);
        prompt = "If I had one thing I could do over today, what would it be?";
        prompts.Add(prompt);
        prompt = "What was a struggle that I overcame today?";
        prompts.Add(prompt);
        prompt = "What was the proudest moment of my day?";
        prompts.Add(prompt);
        prompt = "Did I spend any time treating myself today?";
        prompts.Add(prompt);
        prompt = "What is one thing you would like to tell your future self?";
        prompts.Add(prompt);

        Console.WriteLine("Welcome To your personal Journal program!");
        Console.WriteLine("When the menu pops up simply select a number, after the word input");
        Console.WriteLine("to navigate to different items. I would start by writing an entry, after that");
        Console.WriteLine("explore, and remember. Have fun!");

        Journal newJournal = new Journal();

        do
        {
            answer = Menu();
            if (answer == 1)
            { 
                string phrase = getPrompt(prompts);
                Console.WriteLine(phrase);
                Console.Write("> ");
                string response = Console.ReadLine();
                Entry newEntry = new Entry();
                newEntry._date = dateString;
                newEntry._prompt = phrase;
                newEntry._response = response;
                string theEntry = newEntry.AddToEnteries();

                newJournal._entries.Add(theEntry);
                newJournal._entries.Add("");

                // WriteFunction()
            }
            else if (answer == 2)
            {
                newJournal.DisplayList();
            }
            else if (answer == 3)
            {
                newJournal.Load();
            }
            else if (answer == 4)
            {
                newJournal.Save();
            }
            else if (answer == 5)
            {
                string dafile = "schedule.txt";
                bool hasLines = !File.ReadLines(dafile).Any();
                if (hasLines == false)
                {
                    Console.WriteLine("This is your previous scheduling: ");
                    Console.WriteLine(" "); 
                    dafile = "schedule.txt";
                    
                    string schedule = File.ReadAllText(dafile);

                    Console.WriteLine(schedule);
                }
                Console.WriteLine("Here you can write your schedule, dont worry.");
                Console.WriteLine("We will help you stay on top of things.");
                Console.WriteLine($"Today is the {day} of {month}, keep that in mind as you plan");
                Console.WriteLine("Type in 0 when your finished.");

                string filepath = "schedule.txt";
                string input = "";
                string row;
                Console.Write("> ");
                while ((row = Console.ReadLine()) != "0")
                {
                    input += row + Environment.NewLine + Environment.NewLine;
                    Console.Write("> ");
                }

                File.WriteAllText(filepath, input);

                Console.WriteLine("Your schedule has been updated.");
            }
            else if (answer == 6)
            {

                Console.WriteLine("Remember your schedule! ");
                    Console.WriteLine(" "); 
                    string dafile = "schedule.txt";
                    string schedule = File.ReadAllText(dafile);

                    Console.WriteLine(schedule);
                Console.WriteLine(); 
                Console.WriteLine("See you again soon!");
            }
            else 
            {
                Console.WriteLine(" --- Invalid input ---");
            }
            Console.WriteLine("");

        }   while (answer != 6);
    }
}