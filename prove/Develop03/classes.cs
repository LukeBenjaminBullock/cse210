using System.Text.RegularExpressions;

// ! Class 1 
public class Scripture
{
    private string _scriptureHead = "";
    private List<string> _originalVerse = new List<string>(); 

    public Scripture()
    {

    }

    public void GetValues()
    {
        string filePath = "scripture.txt";
        List<string> firstLines = new List<string>();
        List<string> otherLines = new List<string>();

        using (StreamReader reader = File.OpenText(filePath))
        {
            string text = reader.ReadToEnd();
            string[] sections = text.Split(new string[] { "---" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < sections.Length; i++)
            {
                string[] lines = sections[i].Split(new string[] { "\r\n", "\n" },
                    StringSplitOptions.None);
                firstLines.Add(lines[1]);

                string currentItem = "";

                for (int j = 2; j < lines.Length; j++)
                {
                    currentItem += lines[j];
                    currentItem += " ";
                }
                otherLines.Add(currentItem);
            }
        }

        Random rand = new Random();
        int firstLineCount = firstLines.Count;
        int randomIndex = rand.Next(0, firstLineCount);

        string[] verse = otherLines[randomIndex].Split(new string[] { " " }, StringSplitOptions.None);

        _scriptureHead = firstLines[randomIndex];

        _originalVerse.AddRange(verse); 

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
public class Word
{
    public List<string> _oldVerse = new List<string>(); 
    private List<int> _blankIndexes = new List<int>(); 
    private List<string> _modifiedVerse = new List<string>(); 
    private int _listLength = 0;
    private int _randIndex = 0;
    private int _wordLength = 1;
    private bool _changed = false;

    public Word()
    {

    }

    public void GetOldVerse()
    {
        Scripture scripture1 = new Scripture();
        scripture1.GetValues(); 
        _oldVerse = scripture1.ReturnVerse();
    }

    public void ModifyWord()
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

public class Reference
{
    private string _head = "";
    private List<string> _verse = new List<string>();
    Word scripture;

    public Reference()
    {

    }

    public void InitializeValue()
    {
        Scripture oldScripture = new Scripture();
        oldScripture.GetValues();
        _head = oldScripture.ReturnHead();
        _verse = oldScripture.ReturnVerse(); 
        scripture = new Word();
        scripture.GetOldVerse();
        _verse = scripture._oldVerse;
    }

    public void UpdateValue()
    {
        scripture.ModifyWord();
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
