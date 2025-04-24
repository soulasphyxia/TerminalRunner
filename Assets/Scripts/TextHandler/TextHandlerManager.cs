using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TextHandlerManager : MonoBehaviour
{   
    [SerializeField] private TMP_Text displayText;

    private Command currentCommand;

    [SerializeField] private EasyModeCommandGenerator commandGenerator;

    private Stage currentStage;

    private int[] currentDifficulties = new int[] { 0, 1 };

    public void OnDisplayText()
    {
        displayText.gameObject.SetActive (true);
    }

    public void OffDisplayText()
    {
        displayText.gameObject.SetActive(false);
    }

    private void Start()
    {
        this.gameObject.SetActive(false);
        OffDisplayText();
        currentCommand = commandGenerator.GenerateCommand();
        DisplayText();
    }

    void Update()
    {
        string input = Input.inputString;
        CommandWord currentWord = currentCommand.CurrentWord;
        CommandCharacter currentChar = currentWord.CurrentChar;
        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];
            if (char.IsLetterOrDigit(c) || char.IsSymbol(c) || char.IsPunctuation(c))
            {
                if (currentWord.IsCompleted)
                {
                    currentWord.AddChar(c);
                } 
                else
                {
                    if (c == currentChar.Character)
                    {
                        currentWord.SetCurrentCharCorrectColor();
                        GameEvents.CallAddCombo();
                    }
                    else
                    {
                        currentWord.SetCurrentCharIncorrectColor();
                        GameEvents.CallResetCombo();
                    }
                }
                currentWord.Next();
            }
            else if (c == ' ')
            {
                if (currentWord.IsCompleted)
                {
                    currentCommand.MoveToNextWord();
                }
            }
            else if (c == '\b')
            {
                if (currentWord.IsFirstChar())
                {
                    if (currentCommand.HasErrors) {
                        currentCommand.MoveToPrevWord();
                    }
                }
                else
                {
                    if (currentChar.PositionType == CharacterPositionType.OutWord)
                    {
                        currentWord.DeleteChar();
                    }
                    else
                    {
                        currentWord.RemoveCurrent();
                    }
                }
            }
            DisplayText();
        }
        if (currentCommand.IsCorrect)
        {
            GameEvents.CallOnEnemyDestroyed(currentCommand.DeathAnimation);
            DisplayNewCommand();   
        }
    }

    private void DisplayNewCommand()
    {
        currentCommand = commandGenerator.GenerateCommand();
        DisplayText();
    }

    private void DisplayText()
    {
        displayText.text = currentCommand.ToString();
    }

    private void OnDisable()
    {
        GameEvents.OnChangeStage -= (stage) => HandleStageChanged(stage);
        displayText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameEvents.OnChangeStage += (stage) => HandleStageChanged(stage);
        displayText.gameObject.SetActive(true);
    }

    private void HandleStageChanged(IStageable stage)
    {
        StartCoroutine(DisableComponentCoroutine(3.5f));
        this.currentDifficulties = stage.GetDifficulties();

    }

    private System.Collections.IEnumerator DisableComponentCoroutine(float seconds)
    {
        this.enabled = false;
        
        yield return new WaitForSeconds(seconds);

        this.enabled = true;
    }
}