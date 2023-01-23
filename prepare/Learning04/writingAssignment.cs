public class WritingAssignment : Assignment
{
    private string _title = "";

    public void SetTitle(string title)
    {
        _title = title; 
    }

    public string GetWritingInformation()
    {
        string name = GetStudentName();
        string text = $"{_title} by {name}";
        return text;
    }
}