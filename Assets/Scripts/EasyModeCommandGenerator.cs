using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EasyModeCommandGenerator : ICommandGenerator
{

    private List<string> _commands = new List<string>()
{
    "ls -l",
    "pwd",
    "touch file.txt",
    "rm file.txt",
    "cp file1.txt file2.txt",
    "mv file.txt /home/user/",
    "chmod 755 script.sh",
    "find /home -name '*.txt'",
    "cat file.txt",
    "grep 'pattern' file.txt",
    "df -h",
    "du -sh folder/",
    "ps aux",
    "netstat -tulnp"
};

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
