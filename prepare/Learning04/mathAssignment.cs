public class MathAssignment : Assignment
{
    private string _textbookSection = "";
    private string _problems = "";

    public void SetTextBookSection(string textbookSection)
    {
        _textbookSection = textbookSection;
    }

    public void SetProblems(string problems)
    {
        _problems = problems;
    }

    public string GetHomeworkList()
    {
        return "Section " + _textbookSection + " Problems " + _problems;
    }
}