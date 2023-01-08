using System;

class Program
{
    static void Main(string[] args)
    {
        Random randomGenerator = new Random(); 
        int magicNumber; 

        string input;
        int inputInt;
        int guessCount = 0;
        string answer = "yes";

        do {
            magicNumber = randomGenerator.Next(1,100);
            guessCount = 0; 
            do {
                guessCount ++;
                Console.Write("What is your guess? ");
                input = Console.ReadLine();
                inputInt = int.Parse(input);

                if (inputInt < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (inputInt > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                    Console.WriteLine($"It only took you {guessCount} tries.");
                }
            } while (inputInt != magicNumber);
            Console.Write("Would you like to play again? ");
            answer = Console.ReadLine();
        } while (answer == "yes");

    }
}