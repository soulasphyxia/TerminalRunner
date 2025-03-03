using UnityEngine;

public class CommandCharacter : ColoredCharacter
{
    private CharacterPositionType _positionType;

    public CommandCharacter(char c, string color, CharacterPositionType positionType) : base(c, color)
    {
        this._positionType = positionType;
    }

    public CharacterPositionType PositionType 
    {
        get { return _positionType; }
        set {  _positionType = value; } 
    }

    public void SetColor(string color)
    {
        this.Color = color;
    }
}
