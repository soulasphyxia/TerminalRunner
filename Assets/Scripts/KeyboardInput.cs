using UnityEngine;
using TMPro;

public class KeyboardInputTMP : MonoBehaviour
{
    // Ссылка на текстовый объект TextMeshPro
    public TMP_Text displayText;

    // Переменная для хранения введенного текста
    private string inputText = "";

    void Update()
    {
        // Проверяем нажатие клавиш
        foreach (char c in Input.inputString)
        {
            // Если нажата буква, цифра, знак препинания или пробел
            if (char.IsLetterOrDigit(c) || char.IsPunctuation(c) || char.IsSymbol(c) || c == ' ')
            {
                inputText += c; // Добавляем символ к строке
            }
            // Если нажата клавиша Backspace
            else if (c == '\b' && inputText.Length > 0)
            {
                inputText = inputText.Substring(0, inputText.Length - 1); // Удаляем последний символ
            }
            // Если нажат Enter, можно очистить строку
            else if (c == '\n' || c == '\r')
            {
                inputText = ""; // Очищаем текст
            }
        }

        // Обновляем текст в TextMeshPro
        if (displayText != null)
        {
            displayText.text = inputText;
        }
    }
}