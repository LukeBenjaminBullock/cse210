using System;

class Program
{
    static void Main(string[] args)
    {
        List<Shape> shapes = new List<Shape>();

        Square square = new Square("blue", 5);

        shapes.Add(square);

        Rectangle rectangle = new Rectangle("green", 3, 9);

        shapes.Add(rectangle);

        Circle circle = new Circle("yellow", 2);

        shapes.Add(circle);

        foreach (Shape shape in shapes)
        {
            string color = shape.GetColor();
            double area = shape.GetArea(); 
            Console.WriteLine($"The color is: {color}");
            Console.WriteLine($"The area is: {area}");
        }
        
    }
}