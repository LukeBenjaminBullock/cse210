using System;


class Program
{
    static void Main(string[] args)
    {
        // ! Declaring variables
        string answer = "";
        bool value = true;

        DisplayScripture scripture = new DisplayScripture();
        scripture.InitializeValue(); 

        do
        {
            scripture.DisplayValue();
            answer = Console.ReadLine();

            value = scripture.CheckVerse();

            if (value == true)
            {
                answer = "quit";
            }

            if (answer == "")
            {
                scripture.UpdateValue();
            }
            else if (answer == "quit")
            {
                Console.WriteLine(" ");
                Console.WriteLine("Good work today!");
            }
            else 
            {
                Console.WriteLine(" --- Invalid input ---");
            }

        } while (answer != "quit");
    }
}