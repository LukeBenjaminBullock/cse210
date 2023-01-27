
public class Activity
{
    protected string _startingMessage;
    protected string _endingMessage; 
    // private int _time;
    protected string _activityName; 
    // protected string _description; // ! Why does this exist?
    // protected int _duration;
    protected int _timeInput;
    protected int _frame;
    private List<string> _thoughts = new List<string>() 
    {"peace", "relax", "calm", "silence", "love", "feel"};
    private Random random = new Random();
    private string _thought;

    public Activity(string startMessage, string endMessage, string name){
        startMessage = _startingMessage;
        endMessage = _endingMessage;
        name = _activityName;
    }

    public void SetName(string name){
        name = _activityName;
    }

    public void StartingMessage(){
        Console.WriteLine($"Welcome to the {_activityName}.");
        Console.WriteLine(" ");
        Console.WriteLine(_startingMessage);
        Console.WriteLine(" ");
    }

    public void EndingMessage(){
        Console.WriteLine(_endingMessage);
    }

    public void GetThought(){
        int index = random.Next(_thoughts.Count);
        string thought = _thoughts[index];
        _thought = thought;
    }

    public void Thinking(){
        if (_frame % 12 == 11) {
            this.ClearLine();
            Console.Write(_thought);
            Console.Write("...");
            return;
        }
        else if (_frame % 12 == 8) {
            this.ClearLine();
            Console.Write(_thought);
            Console.Write("..");
            return;
        }
        else if (_frame % 12 == 4) {
            this.ClearLine();
            Console.Write(_thought);
            Console.Write(".");
            return;
        }
    }

    public void GetTimeInput(){
        Console.Write("How long, in seconds, would you like for your session? ");
        string timeString = Console.ReadLine();
        _timeInput = int.Parse(timeString);
    }

    public void StartingScreen(){
        this.StartingMessage();
        this.GetTimeInput();  
    } 

    public void IncrimentFrame(){ // Runs through one frame every 1 tenth of a second.
        Thread.Sleep(100);
        _frame += 1;
    }

    public void ClearLine(){
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(Enumerable.Repeat<char>(' ', Console.BufferWidth).ToArray());
        Console.SetCursorPosition(0, Console.CursorTop);
    }
}

// - CountdownTimer() This will run through the frame once per timer.  

// ! subclass 1 
public class BreathingActivity : Activity
{   

    public BreathingActivity(string startMessage, string endMessage, string name) : 
    base(startMessage, endMessage, name)
    {
        _startingMessage = startMessage;
        _endingMessage = endMessage;
        _activityName = name;
    }

    public void Script()
    {
        Console.Clear();
        Console.WriteLine("Get ready...");
        Console.WriteLine(" ");

        this.GetThought();
        for (int i = 0; i < 60; i++)
        {
            this.IncrimentFrame();
            this.Thinking();
        }

        this.ClearLine();

        for (int i = 0; i < (_timeInput / 10); i++)
        {
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            for (int y = 4; y > -1; y--)
            {
                this.ClearLine();
                Console.Write($"Breath in...{y}");
                if (y == 0)
                {   
                    this.ClearLine();
                    Console.Write("Breath in...");
                }
                Thread.Sleep(1000);

            }
            Console.WriteLine(" ");
            for (int y = 6; y > -1; y--)
            {
                this.ClearLine();
                Console.Write($"Breath out...{y}");
                Thread.Sleep(1000);
            }
        }
    }

}

// - GetTimeInput()
// - Breathing()
// - Frame()

// * ReflectingActivity 
// ? List<string> : _reflecionPrompts 
// ? List<string> :  _randomQuestions 
// - RandomPrompt()
// - RandomQuestion()
// - Display() - Could this be used for all the classes? 
// - DisplayQuestions() 
// - Frame()

// * ListingActivity
// - RandomPrompt() 
// - ListPrompts()
// - ListCount() 
// - Frame()

