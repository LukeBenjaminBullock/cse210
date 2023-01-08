using System;

class Program
{
    static void Main(string[] args)
    {
        // I find it so cool that you can use the functions before you declare them in C++
        DisplayWelcome();
        string username = PromptUserName();
        int userNumber = PromptUserNumber(); 
        int numberSquared = SquareNumber(userNumber);
        DisplayResult(username, numberSquared);


        static void DisplayWelcome()
        {
            Console.WriteLine("Welcome to the program!");
        }

        static string PromptUserName()
        {
            Console.Write("Please enter your name: ");
            string username = Console.ReadLine();
            return username;
        }

        static int PromptUserNumber()
        {
            Console.Write("Please enter your favorite number: ");
            string favoriteNumber = Console.ReadLine(); 
            int favoriteInt = int.Parse(favoriteNumber);
            return favoriteInt;
        }

        static int SquareNumber(int number)
        {
            int numberSquared = number * number; 
            return numberSquared;
        }

        static void DisplayResult(string username, int number)
        {
            Console.WriteLine($"{username}, the square of your number is {number}");
        }
        
    }
}