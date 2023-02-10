using System.Collections.Generic;

public class Screen
{
    private int _screenWidth; 
    private int _screenHeight; 

    public int GetWidth()
    {
        return _screenWidth; 
    }

    public int GetHeight()
    {
        return _screenHeight;
    }

    public void CheckSize()
    {
        _screenWidth = Console.BufferWidth - 1;
        _screenHeight = Console.BufferHeight - 1;
    }
}

public class Object
{
    protected int _x; 
    protected int _y; 
    protected int _width; 
    protected int _height; 
    protected List<int> _rect; 
    protected List<string> _drawing; 
    protected bool _destroyed; 



    public Object (int x, int y, List<string> drawing)
    {
        _x = x;
        _y = y; 
        _drawing = drawing; 
    }
        


    public void SetImage(List<string> drawing)
    {
        _drawing = drawing;
    }

    public void SetLocation(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public void SetDimensions()
    {
        int width = 0; 
        int height = 0; 
        foreach (string line in _drawing)
        {
            height += 1; 
            if (line.Length >= width)
            {
                width = line.Length;
            }
        }

        _width = width;
        _height = height;

        int left = _x;
        int right = _x + _width; 
        int top = _y;
        int bottom = _y + _height; 
        List<int> rect = new List<int>
        {left, right, top, bottom};

        _rect = rect; 
    }

    public int GetX()
    {
        return _x;
    }

    public int GetY()
    {
        return _y;
    }

    public int GetWidth()
    {
        return _width; 
    }

    public int GetHeight()
    {
        return _height;
    }

    public void Draw()
    {
        int counter = 1;
        Console.SetCursorPosition(_x, _y);
        foreach (string line in _drawing)
        {
            for (int i = 0; i < line.Length; i++)
            {
            Console.Write(line[i]);
            }

            Console.SetCursorPosition(_x, (_y + counter));
            counter ++;
        }
    }

    public void Clear()
    {
        int counter = 1;
        Console.SetCursorPosition(_x, _y);
        foreach (string line in _drawing)
        {
            for (int i = 0; i < line.Length; i++)
            {
            Console.Write(" ");
            }

            Console.SetCursorPosition(_x, (_y + counter));
            counter ++;
        }
    }

    public bool GetDestroyed()
    {
        return _destroyed;
    }


    public void Destroy()
    {
        _destroyed = true;
    }

    public List<int> GetRect()
    {
        return _rect; 
    }

    public bool DetectCollision(List<int> otherRect)
    {
        int left1 = otherRect[0];
        int right1 = otherRect[1];
        int top1 = otherRect[2];
        int bottom1 = otherRect[3];

        int left2 = _rect[0];
        int right2 = _rect[1];
        int top2 = _rect[2];
        int bottom2 = _rect[3];

        // Check if either the top right corner is overlapping another rectangle
        if (left1 >= left2 && right1 <= right2 && top1 >= top2 && bottom1 <= bottom2)
        {
            return true;
        }

        if (left2 >= left1 && right2 <= right1 && top2 >= top1 && bottom2 <= bottom1)
        {
            return true;
        }

        return false;
    }
}

public class Projectile : Object
{
    public Projectile (int x, int y, List<string> drawing) : base(x, y, drawing)
    {
    }
}

public class Enemy : Object
{
    public Enemy (int x, int y, List<string> drawing) : base(x, y, drawing)
    {
    }
}

public class Player : Object
{
    public Player (int x, int y, List<string> drawing) : base(x, y, drawing)
    {
    }
}

public class Structures : Object
{
    public Structures (int x, int y, List<string> drawing) : base(x, y, drawing)
    {
    }
}

public class Background : Object
{
    public Background (int x, int y, List<string> drawing) : base(x, y, drawing)
    {
    }
}

// ! change the functionality for this class at some point. 
public class LoadScreen
{

}


public class Animation 
{
    private int _counter = 0; 
    private int _animationFrames; 
    private int _timesAnimated = 0; // ! This is for things like bullets, where youll need to know the amount of times the animation has happened.

    public void SetFrames(int frames)
    {
        _animationFrames = frames; 
    }

    public bool Animate(int frameCounter)
    {
        if (frameCounter <= 100 && _timesAnimated >= (3000 / _animationFrames))
        {
            _counter = 0;
            _timesAnimated = 0; 
        }
        if (frameCounter >= (_counter + _animationFrames))
        {   
            _counter += _animationFrames;
            _timesAnimated ++; 
            return true;
        }
        else {
            return false; 
        }
    }

    public int GetTimes()
    {
        return _timesAnimated; 
    }
}