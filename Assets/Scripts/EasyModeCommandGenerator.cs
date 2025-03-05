using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class EasyModeCommandGenerator : MonoBehaviour, ICommandGenerator
{
    private DatabaseManager _dbManager;
    [SerializeField] private CommandsRepository _commandsRepo;

    private List<string> _commands = new List<string>(){"NadoIspravit"};

    // public void Awake()
    // {
    //     _commandsRepo = GetComponent<CommandsRepository>();
    // } 

    public Command GenerateCommand()
    {
        Debug.Log("Запуск GenerateCommand()");
        getCommandsFromDB();
        return GetCommand(_commands[Random.Range(0, _commands.Count)]);
       //return GetCommand(_commands[0]);
    }

    private async void getCommandsFromDB()
    {

        var commands = await _commandsRepo.GetAllCommandsAsync();
        foreach (var command in commands)
        {
            _commands.Add(command.Name);
        }
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
