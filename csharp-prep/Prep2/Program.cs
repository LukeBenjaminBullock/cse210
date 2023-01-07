using System;

class Program
{
    static void Main(string[] args)
    {
        // User input
        Console.Write("What is your grade percentage? ");  
        string input = Console.ReadLine(); 

        // Converting from string to int. 
        int grade = int.Parse(input);

        // variable declarations
        string letter = "nothing";
        string character = "";

        // Calculations 
        if (grade >= 90)
        {
            letter = "A";
        }
        else if (grade >= 80)
        {
            letter = "B";
        }
        else if (grade >= 70)
        {
            letter = "C";
        }
        else if (grade >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        int firstDigit = grade / 10; 

        int secondDigit = grade - (firstDigit * 10);

        if (secondDigit >= 7)
        {
            character = "+";
        }
        else if (secondDigit < 3)
        {
            character = "-";
        }
        else 
        {
            character = "";
        }


        // Exeptions

        if (grade >= 97)
        {
            character = "+";
        }

        if (grade < 53)
        {
            character = "-";
        }

        if (grade >= 57 && grade < 60)
        {
            character = "+";
        }

        // Results 

        Console.WriteLine($"You got an {letter}{character}");

        if (grade >= 70)
        {
            Console.WriteLine("Congradulations, you passed!");
        }
        else 
        {
            Console.WriteLine("Less than 70? You suck.");
        }

    }
}