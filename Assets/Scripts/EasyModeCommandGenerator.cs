using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EasyModeCommandGenerator : ICommandGenerator
{

    private List<string> _commands = new List<string>() { "git pull origin main", "ls -l", "cd . ." };

    public Command GenerateCommand()
    {
        return GetCommand(_commands[Random.Range(0, _commands.Count)]);
       //return GetCommand(_commands[0]);
    }

    private Command GetCommand(string str)
    {
        List<CommandWord> words = new();

        foreach (string word in str.Split(" ")) {

            List<CommandCharacter> chars = new();
            foreach (char c in word)
            {
                chars.Add(new CommandCharacter(c, InputColors.GRAY, CharacterPositionType.InWord));
            }
            words.Add(new CommandWord(chars, false));
        }
        return new Command(words);
    }
}
