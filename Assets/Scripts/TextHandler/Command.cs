using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;

public class Command
{

    private readonly List<CommandWord> words;
    private int currentWordPosition = 0;

    public Command(List<CommandWord> words)
    {
        this.words = words;
    }

    public List<CommandWord> Words
    {
        get { return  this.words; }
    }

    public CommandWord CurrentWord
    {
        get
        {
            return this.words[currentWordPosition];
        }
    }

    public void MoveToNextWord()
    {
        if (currentWordPosition != words.Count - 1)
        {
            currentWordPosition++;
        }
    }

    public void MoveToPrevWord()
    {
        if (currentWordPosition > 0)
        {
            currentWordPosition--;
        }
    }

    public CommandWord GetPrevWord
    {
        get
        {
            if (currentWordPosition > 0)
            {
                return words[currentWordPosition - 1];
            }
            return null;
        }
    }


    public bool HasErrors
    {
        get { return words.GetRange(0, currentWordPosition + 1).Select(x => x.HasErrors()).Any(); }
    }

    public bool IsCorrect
    {
        get
        {
            return words.Aggregate(true, (acc, x) => acc && x.IsCorrect);
        }
    }

    public override string ToString()
    {
        return string.Join(" ", words);
    }

    private int CalculateMaxOutErrors(List<CommandWord> words)
    {
        int totalChars = words.Aggregate(0, (acc, x) => x.Chars.Count);
        return (int) Math.Ceiling(totalChars * 0.1);
    }
}
