// ! Class 1 
public class GetScripture
{
    private string _scriptureHead = "";
    private List<string> _originalVerse = new List<string>(); 

    public GetScripture()
    {

    }

    public void GetValues()
    {
        string filePath = "scripture.txt"; 

        string[] lines = File.ReadAllLines(filePath);

        _scriptureHead = lines[0];

        string verse = String.Join(" ", lines.Skip(1));

        string[] words = verse.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

        _originalVerse = words.ToList(); 

    }

    public string ReturnHead()
    {
        return _scriptureHead;
    }

    public List<string> ReturnVerse()
    {
        return _originalVerse;
    }
}

// ! Class 2
public class RandomizeScripture
{
    private List<string> _oldVerse = new List<string>(); 
    private List<int> _blankIndexes = new List<int>(); 
    private List<string> _modifiedVerse = new List<string>(); 
    private int _listLength = 0;
    private int _randIndex = 0;
    private int _wordLength = 1;
    private bool _changed = false;

    public RandomizeScripture()
    {

    }

    public void GetOldVerse()
    {
        GetScripture scripture1 = new GetScripture();
        scripture1.GetValues(); 
        _oldVerse = scripture1.ReturnVerse();
    }

    public void ModifyScripture()
{
    if (_modifiedVerse.Count == 0)
    {
        _modifiedVerse = new List<string>(_oldVerse);
    }

    _listLength = _modifiedVerse.Count;

    Random random = new Random();

    do
    {
        _changed = false;
        // This is used to  get the random number out of the list length.
        _randIndex = random.Next(0, _listLength);

        // This saves a number to word length equal to the random list index.
        _wordLength = _modifiedVerse[_randIndex].Length;

        if (!_blankIndexes.Contains(_randIndex))
        {
            _modifiedVerse[_randIndex] = new string('_', _wordLength);
            _blankIndexes.Add(_randIndex);
            _changed = true;
        }
    } while (_changed == false);
}


    public List<string> ReturnModified()
    {
        return _modifiedVerse;
    }

}

// ! Class 3 

public class DisplayScripture
{
    private string _head = "";
    private List<string> _verse = new List<string>();
    RandomizeScripture scripture;

    public DisplayScripture()
    {

    }

    public void InitializeValue()
    {
        GetScripture oldScripture = new GetScripture();
        oldScripture.GetValues();
        _head = oldScripture.ReturnHead();
        _verse = oldScripture.ReturnVerse(); 
        scripture = new RandomizeScripture();
        scripture.GetOldVerse();
    }

    public void UpdateValue()
    {
        scripture.ModifyScripture();
        _verse = scripture.ReturnModified();
    }

    public void DisplayValue()
    {
        Console.Clear();
        Console.WriteLine(_head);

        foreach (string word in _verse)
        {
            Console.Write(word);
            Console.Write(" ");
        }
        Console.WriteLine(" ");
        Console.WriteLine(" ");
        Console.Write("Press enter to continue or type 'quit' to finish: ");
    }

    public bool CheckVerse()
    {
        string verse = string.Join("", _verse).TrimEnd();
        bool value = true;
        foreach (char c in verse)
        {
            if (c != '_')
            {
                value = false;
                break;
            }
        }
        return value;
    }
}
