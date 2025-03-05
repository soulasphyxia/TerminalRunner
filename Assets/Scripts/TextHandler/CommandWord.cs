using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class CommandWord
{

    private List<CommandCharacter> chars;
    private bool isCorrect;
    private int currentPosition = 0;

    private string  ERROR_COLOR = InputColors.RED;

    private int errors = 0;

    private readonly int maxOutErrors = 1;

    public CommandWord(List<CommandCharacter> chars, bool isCorrect)
    {
        this.Chars = chars;
        this.IsCorrect = isCorrect;
    }

    public bool IsCorrect {
        get
        {
            return chars.All(x => x.Color == InputColors.BRIGHT_WHITE);
        }
        set
        {
            isCorrect = value;
        }
    }

    public void AddChar(char c)
    {
        if (errors < maxOutErrors)
        {
            this.chars.Add(new CommandCharacter(c, InputColors.DARK_RED, CharacterPositionType.OutWord));
            errors++;
        }
    }

    public bool IsCompleted
    {
        get
        {
            return currentPosition == chars.Count;
        }
    }

    public void DeleteChar()
    {
        this.chars.RemoveAt(chars.Count - 1);
        currentPosition--;
        errors--;
    }

    public void Next()
    {
        if (!IsCompleted)
        {
            currentPosition++;
        }
    }

    public bool IsFirstChar()
    {
        return currentPosition == 0;
    }

    public void RemoveCurrent()
    {
        if (currentPosition > 0)
        {
            currentPosition--;
            SetCurrentCharRemoveColor();
        }
    }

    public void SetCurrentCharCorrectColor()
    {
        CurrentChar.SetColor(InputColors.BRIGHT_WHITE);
    }

    public void SetCurrentCharIncorrectColor()
    {
        CurrentChar.SetColor(ERROR_COLOR);
    }

    public void SetCurrentCharRemoveColor()
    {
        CurrentChar.SetColor(InputColors.GRAY);
    }

    public void SetCurrentCharOutOfWordColor()
    {
        CurrentChar.SetColor(InputColors.DARK_RED);
    }

    public CommandCharacter CurrentChar
    {
        get
        {
            if (!IsCompleted)
            {
                return chars[currentPosition];
            }
            return chars[chars.Count - 1];
        }
    }

    public List<CommandCharacter> Chars {
        get
        {
            return chars;
        }
        set
        {
            chars = value;
        }
    }

    public bool HasErrors()
    {
        return errors > 0 && !isCorrect;
    }

    public override string ToString()
    {
        return string.Join("", chars.Select(c => c.ToString()).ToList());
    }
}
