public class Fraction 
{
    private int _top = 0;
    private int _bottom = 0;

    public void ConstructFraction()
    {
        _top = 1;
        _bottom = 1; 
    }

    public void ConstructFraction(int top)
    {
        _top = top;
        _bottom = 1; 
    }

    public void ConstructFraction(int top, int bottom)
    {
        _top = top; 
        _bottom = bottom; 
    }

    public int GetTop()
    {
        return _top;
    }

    public void SetTop(int top)
    {
        _top = top;
    }

    public int GetBottom()
    {
        return _bottom;
    }

    public void SetBottom(int bottom)
    {
        _bottom = bottom;
    }

    public string GetFractionString()
    {
        string firststring = _top.ToString();
        string secondstring = _bottom.ToString();
        string fractionstring = firststring + "/" + secondstring;
        return fractionstring;
    }

    public int GetDecimalValue()
    {
        int decimalvalue = _top / _bottom; 
        return decimalvalue;
    }

}