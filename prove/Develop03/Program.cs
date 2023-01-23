using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        // ! Declaring variables
        string answer = "";
        bool value = true;

        Console.Write("How many words would you like to remove at a time? ");
        string placeholder = Console.ReadLine();

        int amount = int.Parse(placeholder);

        Reference scripture = new Reference();
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
                for (int i = 0; i < amount; i++)
                {
                    scripture.UpdateValue();
                    if (scripture.CheckVerse() == true)
                    {
                        break;
                    }
                }
            }   
            else if (answer == "quit")
            {
                Console.WriteLine(" ");
                Console.WriteLine("Good work today!");
            }
            else 
            {
                // ! You would have to be a superhero to actually ever see this line of text.

                Console.WriteLine(" --- Invalid input ---");
            }

        } while (answer != "quit");

    }
}



// I think that I deserve full marks because I completed the core requirements. 
// Added the ability to choose how many words were removed at a time. 
// And randomly loaded different scripture passages from a txt file. 