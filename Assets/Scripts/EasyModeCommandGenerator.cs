using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EasyModeCommandGenerator : MonoBehaviour, ICommandGenerator
{
    private List<JsonCommand> commands = new();

    private HashSet<int> currentDifficulties = new (new int[] { 0, 1 });


    private int lastIndex = -1;

    private void Awake()
    {
        GameEvents.OnChangeStage += (stage) => HandleStageChanged(stage);
        commands = JsonLoader.Instance.Commands;
        Debug.Log(commands.Count);
    }

    public Command GenerateCommand()
    {
        var currentCommands = commands.Where(x => currentDifficulties.Contains(x.difficulty)).ToArray();
        int index = Random.Range(0, currentCommands.Length);
        while (index == lastIndex)
        {
            index = Random.Range(0, currentCommands.Length);
        }
        lastIndex = index;
        return GetCommand(currentCommands[index].command);
    }

    private void HandleStageChanged(IStageable stage)
    {
        currentDifficulties.Clear();
        currentDifficulties.AddRange(stage.GetDifficulties());
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
