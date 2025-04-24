using System.Collections;
using TMPro;
using UnityEngine;

public class TextHandlerManager : MonoBehaviour
{
    [SerializeField] private TMP_Text displayText;
    [SerializeField] private EasyModeCommandGenerator commandGenerator;

    private Command currentCommand;

    private void Awake()
    {
        GameEvents.OnChangeStage += HandleStageChanged;
    }

    private void OnDestroy()
    {
        GameEvents.OnChangeStage -= HandleStageChanged;
        StopAllCoroutines();
    }

    public void OnDisplayText()
    {
        displayText.gameObject.SetActive(true);
    }

    public void OffDisplayText()
    {
        displayText.gameObject.SetActive(false);
    }

    private void Start()
    {
        gameObject.SetActive(false);
        OffDisplayText();
        currentCommand = commandGenerator.GenerateCommand();
        DisplayText();
    }

    private void Update()
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
                    if (currentCommand.HasErrors)
                    {
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
            GameEvents.CallOnEnemyDestroyed();
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

    private void HandleStageChanged(IStageable stage)
    {
        if (!this.isActiveAndEnabled) return;
        StartCoroutine(DisableComponentCoroutine(3.5f));
    }

    private IEnumerator DisableComponentCoroutine(float seconds)
    {
        this.enabled = false;
        yield return new WaitForSeconds(seconds);
        if (this != null) this.enabled = true;
    }
}
