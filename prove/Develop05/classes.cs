using System.Xml.Serialization;
// TODO: Goals class

[XmlInclude(typeof(SimpleGoal))]
[XmlInclude(typeof(ChecklistGoal))]
[XmlInclude(typeof(EternalGoal))]
public abstract class Goals 
{
    protected string _goalName; 
    protected string _goalDescription; 
    protected string _goalPoints; 
    protected bool _goalFinished = false; // checkmark 

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

}

// TODO: SimpleGoal class
public class SimpleGoal : Goals
{

    public override int DoGoal(bool decide) 
    {
        _goalFinished = decide; // Decide whether its true or false. 
        int points = int.Parse(_goalPoints);
        return points;
    }
}

// TODO: EternalGoal class
public class EternalGoal : Goals
{
    private bool _neverEnding; 

    public override void GetDetails()
    {
        base.GetDetails();
    }

    public override int DoGoal(bool decide) 
    {
        int points = int.Parse(_goalPoints);
        return points;
    }

}

// TODO: ChecklistGoal class
public class ChecklistGoal : Goals 
{
    private int _numberTimes; 
    private int _counter; 
    private int _bonus; 

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
        if (decide == true)
        {
            _counter += 1; 
        }
        if (_counter == _numberTimes)
        {
            _goalFinished = true;
        }

        int points = int.Parse(_goalPoints);

        if (_goalFinished == true)
        {
            points += _bonus;
        }
        return points;
    }

    public string GetCounter()
    {
        string str = _counter.ToString();
        return str;
    }

    public string GetTimes()
    {
        string str = _numberTimes.ToString();
        return str;
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
        XmlSerializer serializer = new XmlSerializer(typeof(List<Goals>));

        using (FileStream stream = new FileStream(_fileName, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine(_totalPoints);
                serializer.Serialize(writer, _goals);
            }
        }
    }


    public void Load()
    {
        Console.Write("What is the name for the goal file? ");
        _fileName = Console.ReadLine();
        XmlSerializer serializer = new XmlSerializer(typeof(List<Goals>));
        using (FileStream stream = new FileStream(_fileName, FileMode.Open))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                // Read the first line of the file
                string totalPointsString = reader.ReadLine();
                // Convert the first line to an integer
                _totalPoints = int.Parse(totalPointsString);
                // Deserialize the rest of the file
                _goals = (List<Goals>)serializer.Deserialize(reader);
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
                string counter = child.GetCounter();
                string total = child.GetTimes(); 
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
}

