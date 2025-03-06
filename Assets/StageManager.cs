using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{ 
    [SerializeField] private EnemySpawner spawner;
    private SpriteRenderer backgroundRenderer;
    [SerializeField] TMP_Text text;
    [SerializeField] private Image fadePanel;
    private bool isFading = false;
    int currentStage;

    public void Awake()
    {
        GameEvents.OnChangeStage += NextStage;
    }

    void Start()
    {

        backgroundRenderer = GameObject.FindGameObjectWithTag("Background").GetComponent<SpriteRenderer>();

        // Устанавливаем начальную прозрачность панели затемнения
        if (fadePanel != null)
        {
            fadePanel.color = new Color(0, 0, 0, 0); // Полностью прозрачный
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void NextStage(IStageable stage) {
        if (stage.IsLastStage())
        {
            text.text = $"Этап {stage.GetStageNumber() - 1} пройден!\nБесконечный режим!";
            TransitionToStage(stage);
            return;
        }
        string displayText = $"Этап {stage.GetStageNumber() - 1} пройден!";
        
        int currentStage = stage.GetStageNumber();
        text.text = displayText;
        TransitionToStage(stage);
    }

    public void TransitionToStage(IStageable stage)
    {
        if (isFading) return; // Если уже идет процесс затемнения, не запускаем новый
        spawner.CanSpawn = false;
        spawner.EnemiesSpeed += 1f;
        spawner.CanSpawn = true;
        StartCoroutine(FadeAndChangeStage(stage));
    }

    // Корутина для затемнения, смены фона и возвращения к нормальной видимости
    private System.Collections.IEnumerator FadeAndChangeStage(IStageable stage)
    {
        isFading = true;

        // 1. Затемняем экран и показываем текст одновременно
        StartCoroutine(FadeScreen(1f)); // Затемнение экрана
        yield return StartCoroutine(FadeText(1f)); // Показ текста (ожидаем его завершения)

        // 2. Добавляем задержку перед сменой фона
        yield return new WaitForSeconds(1f); // Задержка в 1 секунду

       backgroundRenderer.sprite = stage.GetBackground();

        // 4. Убираем текст и осветляем экран одновременно
        StartCoroutine(FadeScreen(0f)); // Осветление экрана
        yield return StartCoroutine(FadeText(0f)); // Скрытие текста (ожидаем его завершения)

        isFading = false;
    }

    // Корутина для плавного изменения прозрачности
    private System.Collections.IEnumerator FadeScreen(float targetAlpha)
    {
        float currentAlpha = fadePanel.color.a;
        float duration = 1f; // Длительность затемнения/осветления
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(currentAlpha, targetAlpha, timeElapsed / duration);
            fadePanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // Устанавливаем конечное значение альфа
        fadePanel.color = new Color(0, 0, 0, targetAlpha);
    }

    private System.Collections.IEnumerator FadeText(float targetAlpha)
    {
        float currentAlpha = text.color.a;
        float duration = 0.5f; // Длительность появления/исчезновения текста
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(currentAlpha, targetAlpha, timeElapsed / duration);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            yield return null;
        }

        // Устанавливаем конечное значение альфа
        text.color = new Color(text.color.r, text.color.g, text.color.b, targetAlpha);
    }

}
