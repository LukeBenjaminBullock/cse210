using System;

class Program
{
    static void Main(string[] args)
    {
        ModifyGoals modify = new ModifyGoals();

        string answer = "0"; 
        int displayPoints = 0; 

        do {
            Console.WriteLine("");
            Console.WriteLine($"You have {displayPoints} points.");
            Console.WriteLine("");
            Console.WriteLine("Menu Options:");
            Console.WriteLine("   1. Create New Goal");
            Console.WriteLine("   2. List Goals");
            Console.WriteLine("   3. Save Goals");
            Console.WriteLine("   4. Load Goals");
            Console.WriteLine("   5. Record Event");
            Console.WriteLine("   6. Quit");
            Console.Write("Select a choice from the menu: ");
            answer = Console.ReadLine(); 

            if (answer == "1")
            {
                Console.WriteLine("The types of Goals are: ");
                Console.WriteLine("   1. Simple Goal");
                Console.WriteLine("   2. Eternal Goal");
                Console.WriteLine("   3. Checklist Goal");
                Console.Write("Which type of goal would you like to create? ");
                answer = Console.ReadLine();

                if (answer == "1")
                {
                    SimpleGoal goal = new SimpleGoal(); 
                    goal.GetDetails();
                    modify.AddGoal(goal);
                } 
                else if (answer == "2")
                {
                    EternalGoal goal = new EternalGoal(); 
                    goal.GetDetails(); 
                    modify.AddGoal(goal);
                }
                else if (answer == "3")
                {
                    ChecklistGoal goal = new ChecklistGoal(); 
                    goal.GetDetails(); 
                    modify.AddGoal(goal); 
                }
            } 
            else if (answer == "2")
            {
                Console.WriteLine("The goals are: ");
                modify.DisplayGoals();
            }
            else if (answer == "3")
            {
                modify.Save();
            }
            else if (answer == "4")
            {
                modify.Load(); 
            }
            else if (answer == "5")
            {   
                int goalNumber = 1;
                Console.WriteLine("The goals are:");
                List<Goals> goals = modify.GetGoals(); 
                foreach (Goals goal in goals)
                {   
                    string name = goal.GetName(); 
                    Console.WriteLine($"{goalNumber}. {name}");
                    goalNumber++;
                }
                Console.Write("Which goal did you accomplish? ");
                answer = Console.ReadLine();

                int intAnswer = int.Parse(answer);

                goalNumber = 1;
                foreach (Goals goal in goals)
                {
                    if (goalNumber == intAnswer)
                    {
                        int points = goal.DoGoal(true);
                        modify.AddPoints(points);
                    }
                    goalNumber++;
                }
                Console.WriteLine("Congradulations! You have earned {points} points!");
                Console.WriteLine("You now have {displayPoints} points.");

            }
            else if (answer == "6")
            {
                Console.WriteLine("See you later, Allegator.");
            }
            else 
            {
                Console.WriteLine("Invalid input");
            }
        } while (answer != "6");
    }
}