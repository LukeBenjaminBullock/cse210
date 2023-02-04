using System.Xml.Serialization;
// TODO: Goals class

public abstract class Goals 
{
    protected string _goalName; 
    protected string _goalDescription; 
    protected string _goalPoints; 
    protected bool _goalFinished; // checkmark 
    protected bool _neverEnding; 

    public Goals(string name  = "Name", string description = "Description", string points  = "0", bool finished  = false, bool eternal = false)
    {
        _goalName = name;
        _goalDescription = description;
        _goalPoints = points;
        _goalFinished = finished;
        _neverEnding = eternal;
    }

    public virtual void GetDetails()
    {
        Console.Write("What is the name of your goal? ");
        _goalName = Console.ReadLine();
        Console.Write("What is a short description of it? ");
        _goalDescription = Console.ReadLine(); 
        Console.Write("What is the amount of points associated with this goal? ");
        _goalPoints = Console.ReadLine(); 
    }

    public abstract int DoGoal(bool decide);

    public string GetName()
    {
        return _goalName;
    }

    public string GetDescription()
    {
        return _goalDescription; 
    }

    public bool CheckFinished()
    {
        return _goalFinished;
    }

    public string GetPoints()
    {
        return _goalPoints;
    }

    public bool GetLength()
    {
        return _neverEnding; 
    }

}

// TODO: SimpleGoal class
public class SimpleGoal : Goals
{

    public SimpleGoal(string name  = "Name", string description = "Description", string points  = "0", bool finished  = false, bool eternal = false) : base(name, description, points, finished, eternal)
    {
    }

    public override int DoGoal(bool decide) 
    {
        int points = 0;
        if (this._goalFinished == true)
        {
            Console.WriteLine("Sorry, you allready completed this goal.");
        } else
        {
        points = int.Parse(_goalPoints);
        }
        _goalFinished = decide; // Decide whether its true or false. 
        return points;
    }
}

// TODO: EternalGoal class
public class EternalGoal : Goals
{
    private new bool _neverEnding = true; 

    public EternalGoal(string name  = "Name", string description = "Description", string points  = "0", bool finished  = false, bool eternal = true) : base(name, description, points, finished, eternal)
    {
    }

    public override void GetDetails()
    {
        base.GetDetails();
    }

    public override int DoGoal(bool decide) 
    {
        int points = int.Parse(_goalPoints);
        this._goalFinished = false;
        return points;
    }

}

// TODO: ChecklistGoal class
public class ChecklistGoal : Goals 
{
    private int _numberTimes; 
    private int _counter; 
    private int _bonus; 

    public ChecklistGoal(string name  = "Name", string description = "Description", string points  = "0", bool finished  = false, bool eternal = false, int times = 0, int counter = 0, int bonus = 0) : base(name, description, points, finished, eternal)
    {
        _numberTimes = times;
        _counter = counter;
        _bonus = bonus;
    }

    public override void GetDetails()
    {
        Console.Write("What is the name of your goal? ");
        _goalName = Console.ReadLine();
        Console.Write("What is a short description of it? ");
        _goalDescription = Console.ReadLine(); 
        Console.Write("What is the amount of points associated with this goal? ");
        _goalPoints = Console.ReadLine(); 
        Console.Write("How many times does this goal need to be accomplished for a bonus? ");
        string answer = Console.ReadLine();
        _numberTimes = int.Parse(answer);
        Console.Write("What is the bonus for accomplising it that many times? ");
        answer = Console.ReadLine();
        _bonus = int.Parse(answer);
    }

    public override int DoGoal(bool decide) 
    {   
        int points = 0;
        if (this._goalFinished == true)
        {
            Console.WriteLine("Sorry, you allready completed this goal.");
        } else
        {
            if (decide == true)
            {
                _counter += 1; 
            }
            if (_counter == _numberTimes)
            {
                _goalFinished = true;
            }

            points = int.Parse(_goalPoints);

            if (_goalFinished == true)
            {
                points += _bonus;
            }
        }
        return points;
    }

    public int GetCounter()
    {
        return _counter;
    }

    public int GetTimes()
    {
        return _numberTimes;
    }

    public int GetBonus()
    {
        return _bonus;
    }

}

// TODO: ModifyGoals class 
public class ModifyGoals
{
    private List<Goals> _goals = new List<Goals>();
    private string _fileName;
    private int _totalPoints = 0; 

    public void AddGoal(Goals goal)
    {
        _goals.Add(goal);
    }

    public void Save()
    {
        Console.Write("What is the name for the goal file? ");
        _fileName = Console.ReadLine();
        using (FileStream stream = new FileStream(_fileName, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {   
                writer.WriteLine($"{_totalPoints}");
                foreach (Goals goal in _goals)
                {   
                    string name = goal.GetName(); 
                    string description = goal.GetDescription();
                    string points = goal.GetPoints();
                    bool finished = goal.CheckFinished();
                    bool eternal = goal.GetLength();

                    if (goal is ChecklistGoal child)
                    {
                        int times = child.GetTimes();
                        int counter = child.GetCounter();
                        int bonus = child.GetBonus();
                        writer.WriteLine($"{name}, {description}, {points}, {finished}, {eternal}, {times}, {counter}, {bonus}");
                    } else
                    {
                        writer.WriteLine($"{name}, {description}, {points}, {finished}, {eternal},");
                    }
                }
            }
        }
    }



    public void Load()
    {
        Console.Write("Enter the name of the goal file: ");
        _fileName = Console.ReadLine();
        using (FileStream stream = new FileStream(_fileName, FileMode.Open))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                _totalPoints = int.Parse(reader.ReadLine());
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] parts = line.Split(',');

                    string name = parts[0];
                    string description = parts[1];
                    string points = parts[2];
                    bool finished = bool.Parse(parts[3]);
                    bool eternal = bool.Parse(parts[4]);

                    if (parts.Length == 8)
                    {
                        int times = int.Parse(parts[5]);
                        int counter = int.Parse(parts[6]);
                        int bonus = int.Parse(parts[7]);
                        ChecklistGoal child = new ChecklistGoal(name, description, points, finished, eternal, times, counter, bonus);
                        
                        _goals.Add(child);
                    }
                    else if (eternal == true)
                    {
                        EternalGoal child = new EternalGoal(name, description, points, eternal, finished);
                        
                        _goals.Add(child);
                    }
                    else
                    {
                        SimpleGoal child = new SimpleGoal(name, description, points, eternal, finished);
                        
                        _goals.Add(child);
                    }
                }
            }
        }
    }



    public void DisplayGoals()
    {
        int goalNumber = 1;
        foreach (Goals goal in _goals)
        {   
            string endTag = "";
            if (goal is ChecklistGoal child)
            {
                int counterInt = child.GetCounter();
                int totalInt = child.GetTimes(); 
                string counter = counterInt.ToString();
                string total = totalInt.ToString(); 
                endTag = "-- Currently completed: " + counter + "/" + total;
            }

            string name = goal.GetName(); 
            string description = goal.GetDescription();
            string checkbox = "[ ]";
            if (goal.CheckFinished() == true)
            {
                checkbox = "[X]";
            }
            Console.WriteLine($"   {goalNumber}. {checkbox} {name} ({description}) {endTag}");
            goalNumber++;
        }
    }

    public List<Goals> GetGoals()
    {
        return _goals;
    }

    public void AddPoints(int points)
    {
        _totalPoints += points;
    }

    public void RemovePoints(int points)
    {
        _totalPoints -= points;
    }

    public int GetPoints()
    {
        return _totalPoints;
    }
}

