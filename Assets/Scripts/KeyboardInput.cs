using UnityEngine;
using TMPro;

public class KeyboardInputTMP : MonoBehaviour
{
    // ������ �� ��������� ������ TextMeshPro
    public TMP_Text displayText;

    // ���������� ��� �������� ���������� ������
    private string inputText = "";

    void Update()
    {
        // ��������� ������� ������
        foreach (char c in Input.inputString)
        {
            // ���� ������ �����, �����, ���� ���������� ��� ������
            if (char.IsLetterOrDigit(c) || char.IsPunctuation(c) || char.IsSymbol(c) || c == ' ')
            {
                inputText += c; // ��������� ������ � ������
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
            }
        }

        // ��������� ����� � TextMeshPro
        if (displayText != null)
        {
            displayText.text = inputText;
        }
    }
}