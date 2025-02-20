using TMPro;
using UnityEngine;

public class TextHandlerManager : MonoBehaviour
{

    [SerializeField] private TMP_Text command;
    [SerializeField] private TMP_Text displayText;
    private string inputText = "";

    private Color BRIGHT_WHITE;
    private Color RED;

    private char[] commandChars;

    private void Awake()
    {
        string[] commands = new string[] {"git push origin", "git pull origin main", "ls -l", "cd . ."};

        command.text = commands[Random.Range(0, commands.Length)];

        commandChars = command.text.ToCharArray();

        BRIGHT_WHITE = Color.white;
        BRIGHT_WHITE.a = 255;
        RED = Color.red;
        RED.a = 255;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string input = Input.inputString;
        int currentIndex = 0;

        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];
            // ���� ������ �����, �����, ���� ���������� ��� ������
            if (char.IsLetterOrDigit(c) || char.IsPunctuation(c) || char.IsSymbol(c) || c == ' ')
            {
                if (c.Equals(commandChars[currentIndex]))
                {
                    SetCharacterColor(currentIndex, BRIGHT_WHITE);
                } else
                {
                    SetCharacterColor(currentIndex, RED);
                }
                inputText += c; // ��������� ������ � ������
                currentIndex++;
            }
            // ���� ������ ������� Backspace
            else if (c == '\b' && inputText.Length > 0)
            {
                inputText = inputText.Substring(0, inputText.Length - 1); // ������� ��������� ������
            }
            // ���� ����� Enter, ����� �������� ������
            else if (c == '\n' || c == '\r')
            {
                inputText = ""; // ������� �����
                currentIndex++;
            }
        } 
        DisplayInput(inputText);
    }

    private void DisplayInput(string inputText)
    {
        if (displayText != null)
        {
            Color brightColor = Color.white;
            brightColor.a = 255;
            displayText.color = brightColor;
            displayText.text = inputText;
        }
    }

    private void SetCharacterColor(int index, Color color)
    {
        displayText.ForceMeshUpdate(); // ��������� ��� ������
        TMP_TextInfo textInfo = displayText.textInfo;

        if (index < 0 || index >= textInfo.characterCount) return;

        int materialIndex = textInfo.characterInfo[index].materialReferenceIndex;
        int vertexIndex = textInfo.characterInfo[index].vertexIndex;

        Color32[] newColors = textInfo.meshInfo[materialIndex].colors32;

        newColors[vertexIndex + 0] = color;
        newColors[vertexIndex + 1] = color;
        newColors[vertexIndex + 2] = color;
        newColors[vertexIndex + 3] = color;

        textInfo.meshInfo[materialIndex].colors32 = newColors;
        displayText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }
}
