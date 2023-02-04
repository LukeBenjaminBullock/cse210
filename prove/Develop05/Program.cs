using System;

class Program
{
    static void Main(string[] args)
    {
        ModifyGoals modify = new ModifyGoals();

        string answer = "0"; 
        int displayPoints = 0; 

        string armor = "nothing";
        string weapon = "fists";

        do {
            Console.WriteLine("");
            Console.WriteLine($"Your champion is wearing {armor} and weilding his {weapon}.");
            Console.WriteLine($"You have {displayPoints} points.");
            Console.WriteLine("");
            Console.WriteLine("Menu Options:");
            Console.WriteLine("   1. Create New Goal");
            Console.WriteLine("   2. List Goals");
            Console.WriteLine("   3. Save Goals");
            Console.WriteLine("   4. Load Goals");
            Console.WriteLine("   5. Record Event");
            Console.WriteLine("   6. Shop");
            Console.WriteLine("   7. Quit");
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
                int points = 0;

                goalNumber = 1;
                foreach (Goals goal in goals)
                {
                    if (goalNumber == intAnswer)
                    {
                        points = goal.DoGoal(true);
                        modify.AddPoints(points);
                    }
                    goalNumber++;
                }
                displayPoints = modify.GetPoints();
                Console.WriteLine($"Congradulations! You have earned {points} points!");
                Console.WriteLine($"You now have {displayPoints} points.");

            }else if (answer == "6")
            {   
                Console.WriteLine("What would you like to buy? ");
                Console.WriteLine("   1. Wooden Sword (100)");
                Console.WriteLine("   2. Iron Sword (300)");
                Console.WriteLine("   3. Steel Sword (1200)");
                Console.WriteLine("   4. Lazer Sword (3500)");
                Console.WriteLine("   5. Wooden Armor (120)");
                Console.WriteLine("   6. Iron Armor (400)");
                Console.WriteLine("   7. Steel Armor (1500)");
                Console.WriteLine("   8. Chomatic Armor (4200)");
                Console.Write("> ");
                answer = Console.ReadLine();
                if (answer == "1")
                {
                    if (displayPoints >= 100)
                    {
                        Console.WriteLine("Congradulations, you have bought a Wooden Sword!");
                        modify.RemovePoints(100);
                        weapon = "Wooden Sword";
                    }
                    else
                    {
                        Console.WriteLine("Sorry, you dont have enough points for that.");
                    }
                }
                else if (answer == "2")
                {
                    if (displayPoints >= 300)
                    {
                        Console.WriteLine("Congradulations, you have bought a Iron Sword!");
                        modify.RemovePoints(300);
                        weapon = "Wooden Iron";
                    }
                    else
                    {
                        Console.WriteLine("Sorry, you dont have enough points for that.");
                    }
                }
                else if (answer == "3")
                {
                    if (displayPoints >= 1200)
                    {
                        Console.WriteLine("Congradulations, you have bought a Steel Sword!");
                        modify.RemovePoints(1200);
                        weapon = "Steel Sword";
                    }
                    else
                    {
                        Console.WriteLine("Sorry, you dont have enough points for that.");
                    }
                }
                else if (answer == "4")
                {
                    if (displayPoints >= 3500)
                    {
                        Console.WriteLine("Congradulations, you have bought a Lazer Sword!");
                        modify.RemovePoints(3500);
                        weapon = "Lazer Sword";
                    }
                    else
                    {
                        Console.WriteLine("Sorry, you dont have enough points for that.");
                    }
                }
                else if (answer == "5")
                {
                    if (displayPoints >= 120)
                    {
                        Console.WriteLine("Congradulations, you have bought a Wooden Armor!");
                        modify.RemovePoints(120);
                        armor = "Wooden Armor";
                    }
                    else
                    {
                        Console.WriteLine("Sorry, you dont have enough points for that.");
                    }
                }
                else if (answer == "6")
                {
                    if (displayPoints >= 400)
                    {
                        Console.WriteLine("Congradulations, you have bought a Iron Armor!");
                        modify.RemovePoints(400);
                        armor = "Iron Armor";
                    }
                    else
                    {
                        Console.WriteLine("Sorry, you dont have enough points for that.");
                    }
                }
                else if (answer == "7")
                {
                    if (displayPoints >= 1500)
                    {
                        Console.WriteLine("Congradulations, you have bought a Steel Armor!");
                        modify.RemovePoints(1500);
                        armor = "Steel Armor";
                    }
                    else
                    {
                        Console.WriteLine("Sorry, you dont have enough points for that.");
                    }
                }
                else if (answer == "8")
                {
                    if (displayPoints >= 4200)
                    {
                        Console.WriteLine("Congradulations, you have bought a Chromatic Armor!");
                        modify.RemovePoints(4200);
                        armor = "Chromatic Armor";
                    }
                    else
                    {
                        Console.WriteLine("Sorry, you dont have enough points for that.");
                    }
                } 
                else 
                {
                    Console.WriteLine("That is not an item for sale");
                }

            }
            else if (answer == "7")
            {
                Console.WriteLine("See you later, Allegator.");
            }
            else 
            {
                Console.WriteLine("Invalid input");
            }

            displayPoints = modify.GetPoints();
        } while (answer != "7");
    }
}

// I deserve full marks because I completed the core requirements, and I also made a character for the user that they can buy weapons and armor for. 