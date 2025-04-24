using TMPro;
using UnityEngine;

public class BlinkingText : MonoBehaviour
{
    [SerializeField] private TMP_Text textMeshPro; // Ссылка на компонент TextMeshPro
    public float blinkDuration = 2f; // Время одного полного цикла мигания (в секундах)
    public bool isBlinking = true; // Включение/выключение эффекта мигания

    private Color originalColor; // Исходный цвет текста
    private float timer = 0f;    // Таймер для отслеживания прогресса мигания

    void Start()
    {
        if (textMeshPro != null)
        {
            originalColor = textMeshPro.color; // Сохраняем исходный цвет текста
        }
    }

    void Update()
    {
        if (!isBlinking || textMeshPro == null) return;

        // Обновляем таймер
        timer += Time.deltaTime;

        // Если таймер превышает продолжительность цикла, сбрасываем его
        if (timer > blinkDuration)
        {
            timer -= blinkDuration;
        }

        // Вычисляем нормализованное значение в диапазоне [0, 1] с использованием синусоиды
        float normalizedTime = timer / blinkDuration; // Нормализованное время в диапазоне [0, 1]
        float alpha = (Mathf.Sin(normalizedTime * Mathf.PI * 2f) + 1f) * 0.5f; // Синусоида в диапазоне [0, 1]

        // Устанавливаем новый цвет с измененной прозрачностью
        textMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
    }

    /// <summary>
    /// Метод для включения эффекта мигания
    /// </summary>
    public void StartBlinking()
    {
        isBlinking = true;
        timer = 0f; // Сбрасываем таймер
    }

    /// <summary>
    /// Метод для остановки эффекта мигания
    /// </summary>
    public void StopBlinking()
    {
        isBlinking = false;
        textMeshPro.color = originalColor; // Возвращаем исходный цвет
    }
}
