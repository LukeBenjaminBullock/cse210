using System;

class Program
{
    static void Main(string[] args)
    {
        static int Menu()
        {
            Console.WriteLine(" 1: Write");
            Console.WriteLine(" 2: Display");
            Console.WriteLine(" 3: Load");
            Console.WriteLine(" 4: Save");  
            Console.WriteLine(" 5: Quit");
            Console.WriteLine("");
            Console.Write("input > ");
            string string_answer = Console.ReadLine();
            Console.WriteLine("");
            int answer = int.Parse(string_answer);
            return answer;
        }

        int answer = 5; 

        do
        {
            answer = Menu();
            if (answer == 1)
            {
                // WriteFunction()
            }
            else if (answer == 2)
            {
                // ReadFunction
            }
            else if (answer == 3)
            {
                // Load function
            }
            else if (answer == 4)
            {
                // Save function
            }
            else if (answer == 5)
            {
                Console.WriteLine("See you again soon!");
                Console.WriteLine("");
            }
            else 
            {
                Console.WriteLine(" --- Invalid input ---");
                Console.WriteLine("");
            }

        }   while (answer != 5);
    }
}