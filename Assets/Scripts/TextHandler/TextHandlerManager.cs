using TMPro;
using UnityEngine;

public class TextHandlerManager : MonoBehaviour
{   
    [SerializeField] private TMP_Text displayText;

    private Command currentCommand;

    private ICommandGenerator commandGenerator;

    public void OnDisplayText()
    {
        displayText.gameObject.SetActive (true);
    }

    public void OffDisplayText()
    {
        displayText.gameObject.SetActive(false);
    }

    private void Awake()
    {
        GameEvents.OnChangeStage += (stage) => HandleStageChanged(stage);
        this.gameObject.SetActive(false);
        OffDisplayText();
        commandGenerator = new EasyModeCommandGenerator();   
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
                    }
                    else
                    {
                        currentWord.SetCurrentCharIncorrectColor();
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
        displayText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        displayText.gameObject.SetActive(true);
    }

    private void HandleStageChanged(IStageable stage)
    {
        StartCoroutine(DisableComponentCoroutine(3.5f));
    }

    private System.Collections.IEnumerator DisableComponentCoroutine(float seconds)
    {
        // Отключаем компонент
        this.enabled = false;
        

        // Ждем указанное количество секунд
        yield return new WaitForSeconds(seconds);

        // Включаем компонент обратно
        this.enabled = true;
    }
}