
public class Activity
{
    protected string _startingMessage;
    protected string _activityName; 
    protected int _timeInput;
    private List<string> _thoughts = new List<string>() 
    {"peace", "relax", "calm", "silence", "love", "feel"};
    protected Random random = new Random();
    private string _thought;

    public Activity(string startMessage, string endMessage, string name){
        startMessage = _startingMessage;
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
        Console.WriteLine($"You have completed another {_timeInput} seconds of the {_activityName}.");
    }

    public void GetThought(){
        int index = random.Next(_thoughts.Count);
        string thought = _thoughts[index];
        _thought = thought;
    }

    public void Thinking(int seconds){

        for (int i = 0; i < seconds; i++)
        {
        Console.Write(_thought);
        Console.Write(".");
        Thread.Sleep(333);
        this.ClearLine();
        
        Console.Write(_thought);
        Console.Write("..");
        Thread.Sleep(333);
        this.ClearLine();
        
        Console.Write(_thought);
        Console.Write("...");
        Thread.Sleep(333);
        this.ClearLine();
        }
    }

    public void SetTimeInput(){
        Console.Write("How long, in seconds, would you like for your session? ");
        string timeString = Console.ReadLine();
        _timeInput = int.Parse(timeString);
    }

    public void StartingScreen(){
        this.StartingMessage();
        this.SetTimeInput();  
    } 

    public void ClearLine(){
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(Enumerable.Repeat<char>(' ', Console.BufferWidth).ToArray());
        Console.SetCursorPosition(0, Console.CursorTop - 1);
    }

    public int GetTimeInput(){
        return _timeInput;
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
        _activityName = name;
    }

    public void Breathing()
    {   
        if (_timeInput < 10)
        {
            _timeInput += 10;
        }
        
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
                    break;
                }
                Thread.Sleep(1000);

            }
            Console.WriteLine(" ");
            for (int y = 6; y > -1; y--)
            {
                this.ClearLine();
                Console.Write($"Breath out...{y}");
                if (y == 0)
                {   
                    this.ClearLine();
                    Console.Write("Breath out...");
                    break;
                }
                Thread.Sleep(1000);
            }
        }
    }


    public void Script()
    {
        Console.Clear();
        Console.WriteLine("Get ready...");
        Console.WriteLine(" ");

        this.GetThought();
        
        this.Thinking(4);
        
        this.ClearLine();

        this.Breathing();

        Console.WriteLine(" ");
        Console.WriteLine(" ");
        Console.WriteLine("Well Done !!");

        this.GetThought();
        this.Thinking(4);

        Console.WriteLine(" ");
        Console.WriteLine(" ");
        this.EndingMessage();

        this.GetThought();
        this.Thinking(4);
    }
}

public class ReflectingActivity : Activity 
{
    private List<string> _reflectionPrompts = new List<string>()
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless.",
        "Think of a time when you took a stand for something you believed in.",
        "Think of a time when you overcame a significant obstacle.",
        "Think of a time when you made a difference in someone's life.",
        "Think of a time when you put someone else's needs before your own.",
        "Think of a time when you made a sacrifice for the greater good.",
        "Think of a time when you helped someone without expecting anything in return.",
        "Think of a time when you showed compassion to someone in need.",
        "Think of a time when you made a positive impact on your community.",
        "Think of a time when you went above and beyond to help someone.",
        "Think of a time when you demonstrated courage in a difficult situation."
    };

    private List<string> _randomQuestions = new List<string>()
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?",
        "What was the most challenging part of this experience?",
        "What did you accomplish as a result of this experience?",
        "What would you do differently if you were to experience this again?",
        "What did you gain from this experience?",
        "What was the impact of this experience on others?",
        "What was the biggest lesson you learned from this experience?",
        "How did this experience change you?"
    }; 
    private string _prompt;
    private string _question;

    public ReflectingActivity(string startMessage, string endMessage, string name) : 
    base(startMessage, endMessage, name)
    {
        _startingMessage = startMessage;
        _activityName = name;
    }

    public void RandomPrompt(){
        int index = random.Next(_reflectionPrompts.Count);
        string prompt = _reflectionPrompts[index];
        _prompt = prompt;
    }

    public void RandomQuestion(){
        int index = random.Next(_randomQuestions.Count);
        string question = _randomQuestions[index];
        _question = question;
    }

    public void Script(){
        Console.WriteLine("Get ready...");
        Console.WriteLine(" ");

        this.GetThought();
        
        this.Thinking(4);
        
        this.ClearLine();

        this.RandomPrompt();

        Console.WriteLine($" --- {_prompt} ---");
        Console.WriteLine(" ");

        Console.WriteLine("When you have something in mind, press enter to continue.");
        Console.ReadLine();

        Console.WriteLine(" ");
        Console.WriteLine("Now ponder on each of the following questions as they related to this experince.");
        for (int y = 5; y > -1; y--)
            {
                this.ClearLine();
                Console.Write($"You may begin in: {y}");
                Thread.Sleep(1000);

            }

        Console.Clear();

        if (_timeInput < 4)
        {
            _timeInput = 4;
        }

        for (int i = 0; i < (_timeInput / 4); i++)
            {
                this.RandomQuestion();
                Console.WriteLine($"> {_question} ");
                this.GetThought();
                this.Thinking(4);
                this.ClearLine();
            }
        Console.WriteLine(" ");
        Console.WriteLine(" ");
        Console.WriteLine("Well Done !!");

        this.GetThought();
        this.Thinking(4);

        Console.WriteLine(" ");
        Console.WriteLine(" ");
        this.EndingMessage();

        this.GetThought();
        this.Thinking(4);
    }
    
}


public class ListingActivity : Activity
{
    private List<string> _randomPrompts = new List<string>()
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "Who are some of your personal heroes?",
        "What are some things that you are grateful for?",
        "What are some challenges you have overcome this week?",
        "What are some things that bring you joy?",
        "What are some things that you want to improve about yourself?",
        "What are some things that you have learned recently?"
    };
    private string _prompt;
    private bool _stopInput = false;

    public int _lineNumber = 0;

    // ! Child Class 3
    public ListingActivity(string startMessage, string endMessage, string name) : 
    base(startMessage, endMessage, name)
    {
        _startingMessage = startMessage;
        _activityName = name;
    }
    public void RandomPrompt(){
        int index = random.Next(_randomPrompts.Count);
        string prompt = _randomPrompts[index];
        _prompt = prompt;
    }

        private void GetInput()
    {
        while (!_stopInput)
        {
            Console.Write("> ");
            string input = Console.ReadLine();
            _lineNumber += 1; 
        }
    }

    public void Script() {
        Console.WriteLine("Get ready...");
        Console.WriteLine(" ");

        this.GetThought();
        
        this.Thinking(4);
        
        this.ClearLine();

        this.RandomPrompt();
        Console.WriteLine("List as many responses as you can to the following prompt:");
        Console.WriteLine($" --- {_prompt} ---");

        for (int y = 5; y > -1; y--)
            {
                this.ClearLine();
                Console.Write($"You may begin in: {y}");
                Thread.Sleep(1000);
                if (y == 0)
                {   
                    this.ClearLine();
                    Console.Write("You may begin in: ");
                    break;
                }

            }
        Console.WriteLine("");
        
        var inputThread = new Thread(() => GetInput());
        inputThread.Start();

        var timerThread = new Thread(() => 
        {
            Thread.Sleep(_timeInput * 1000);
            _stopInput = true;
        });
        timerThread.Start();

        inputThread.Join();
        Console.WriteLine($"You listed {_lineNumber} items!");

        Console.WriteLine(" ");
        Console.WriteLine(" ");
        Console.WriteLine("Well Done !!");

        this.GetThought();
        this.Thinking(4);

        Console.WriteLine(" ");
        Console.WriteLine(" ");
        this.EndingMessage();

        this.GetThought();
        this.Thinking(4);
    }
}

