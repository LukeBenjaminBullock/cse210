using System;

class Program
{
    static void Main(string[] args)
    {
        MathAssignment assignment1 = new MathAssignment();
        assignment1.SetTopic("Math");
        assignment1.SetStudentName("Jennifer Laurence");
        assignment1.SetTextBookSection("7.8");
        assignment1.SetProblems("3-5");
        string homework = assignment1.GetHomeworkList();
        string summary = assignment1.GetSummary();
        Console.WriteLine(summary);
        Console.WriteLine(homework);

        Console.WriteLine(" ");

        WritingAssignment assignment2 = new WritingAssignment();
        assignment2.SetTopic("European History");
        assignment2.SetStudentName("Mary Waters");
        assignment2.SetTitle("The Causes of World War II");
        summary = assignment2.GetSummary();
        string topicHead = assignment2.GetWritingInformation();
        Console.WriteLine(summary);
        Console.WriteLine(topicHead);

    }
}