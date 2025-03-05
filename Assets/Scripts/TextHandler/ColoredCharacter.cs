using TMPro;
using UnityEngine;

public class ColoredCharacter
{
    private char _char;
    private string _color;
    
    public ColoredCharacter(char c, string color)
    {
        _char = c;
        _color = color;
    }

    public char Character
    {
        get { return _char; }
        set { _char = value; }
    }

    public string Color
    {
        get { return _color; }
        set { _color = value; }
    }

    public override string ToString()
    {
        return $"<color={_color}>{_char}</color>";
    }
}
