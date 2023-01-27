using System;

class Program
{
    static void Main(string[] args)
    {   
        string answer = "1";

        do {
            Console.Clear();
            Console.WriteLine("Menu Options: ");
            Console.WriteLine("   1. Start brathing activity");
            Console.WriteLine("   2. Start reflecting activity");
            Console.WriteLine("   3. Start listing activity");
            Console.WriteLine("   4. Quit");
            Console.Write("Select a choice from the menu: ");
            answer = Console.ReadLine();

            if (answer == "1"){
                BreathingActivity activity = new BreathingActivity("will help you relax by walking you though breathing in and out slowly. Clear your mind and focus on your breathing.",
                "Well done!", "Breathing Activity");
                activity.StartingScreen();
                activity.Script();
            }
            else if (answer == "2"){
                Console.WriteLine("Call the reflecing class");
            }
            else if (answer == "3") {
                Console.WriteLine("Call the listing class");
            }
            else if (answer == "4"){
                Console.WriteLine("Have a wonderful day!");
            }

        } while (answer != "4");
    }
}