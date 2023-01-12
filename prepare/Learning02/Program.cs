using System;

class Program
{
    static void Main(string[] args)
    {
        Job job1 = new Job(); 
        job1._company = "Fairfeild";
        job1._jobTitle = "Housekeeer";
        job1._startYear = 1995;
        job1._endYear = 2001; 

        Job job2 = new Job(); 
        job2._company = "Country Blooms";
        job2._jobTitle = "Hired hand";
        job2._startYear = 2006; 
        job2._endYear = 2012;

        Resume resume1 = new Resume(); 
        resume1._name = "Luke Bullock";
        resume1._jobs.Add(job1);
        resume1._jobs.Add(job2);

        resume1.Display();

    }
}