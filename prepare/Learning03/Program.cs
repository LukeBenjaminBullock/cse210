using System;

class Program
{
    static void Main(string[] args)
    {
        Fraction fraction1 = new Fraction();
        fraction1.ConstructFraction();
        string firstnum = fraction1.GetTop().ToString();
        string secondnum = fraction1.GetBottom().ToString();
        Console.WriteLine(firstnum + "/" + secondnum);

        Fraction fraction2 = new Fraction(); 
        fraction2.ConstructFraction(6);
        firstnum = fraction2.GetTop().ToString();
        secondnum = fraction2.GetBottom().ToString();
        Console.WriteLine(firstnum + "/" + secondnum);

        Fraction fraction3 = new Fraction(); 
        fraction3.ConstructFraction(6, 7);
        firstnum = fraction3.GetTop().ToString();
        secondnum = fraction3.GetBottom().ToString();
        Console.WriteLine(firstnum + "/" + secondnum);

        fraction3.SetTop(12);
        fraction3.SetBottom(6);
        Console.WriteLine(fraction3.GetFractionString());

        string decimalvalue = fraction3.GetDecimalValue().ToString();

        Console.WriteLine(decimalvalue);

    }
}