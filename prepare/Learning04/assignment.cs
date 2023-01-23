public class Assignment 
{
    private string _studentName = "";
    private string _topic = "";

    public void SetValues(string studentName, string topic)
    {
        _studentName = studentName;
        _topic = topic;
    }

    public void SetStudentName(string studentName)
    {
        _studentName = studentName;
    }

    public void SetTopic(string topic)
    {
        _topic = topic; 
    }

    public string GetStudentName()
    {
        return _studentName;
    }

    public string GetTopic(string topic)
    {
        return _topic;
    }

    public string GetSummary()
    {
        string summary = _studentName + " - " + _topic;
        return summary;
    }

}