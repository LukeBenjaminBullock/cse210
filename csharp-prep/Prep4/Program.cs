using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        int number; 
        int sum = 0; 
        float average; 
        int largest = -100000000;
        int smallestPositive = 1000000000;
        int tally = 0; 

        List<int> numbers = new List<int>(); 

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        do{
            Console.Write("Enter a number: ");
            string input = Console.ReadLine(); 
            number = int.Parse(input);
            numbers.Add(number);
        }   while (number != 0);

        foreach (int instance in numbers)
        {
            tally += 1;
            sum += instance;
            if (instance > largest)
            {
                largest = instance;
            }
            if (instance > 0 && instance < smallestPositive)
            {
                smallestPositive = instance; 
            }

        }

        average = (float)sum / tally;

        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {largest}");
        Console.WriteLine($"The smallest positive number is: {smallestPositive}");


    }
}