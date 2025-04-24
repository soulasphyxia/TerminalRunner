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

    public void Awake()
    {
        GameEvents.OnChangeStage += NextStage;
    }

    void Start()
    {

        backgroundRenderer = GameObject.FindGameObjectWithTag("Background").GetComponent<SpriteRenderer>();

        if (fadePanel != null)
        {
            fadePanel.color = new Color(0, 0, 0, 0);
        }
    }

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
        if (isFading) return;
        spawner.CanSpawn = false;
        spawner.EnemiesSpeed += 1f;
        spawner.CanSpawn = true;
        StartCoroutine(FadeAndChangeStage(stage));
    }

    private System.Collections.IEnumerator FadeAndChangeStage(IStageable stage)
    {
        isFading = true;
        StartCoroutine(FadeScreen(1f));
        yield return StartCoroutine(FadeText(1f));

        yield return new WaitForSeconds(1f); 

       backgroundRenderer.sprite = stage.GetBackground();

        StartCoroutine(FadeScreen(0f)); 
        yield return StartCoroutine(FadeText(0f));

        isFading = false;
    }

    private System.Collections.IEnumerator FadeScreen(float targetAlpha)
    {
        float currentAlpha = fadePanel.color.a;
        float duration = 1f;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(currentAlpha, targetAlpha, timeElapsed / duration);
            fadePanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadePanel.color = new Color(0, 0, 0, targetAlpha);
    }

    private System.Collections.IEnumerator FadeText(float targetAlpha)
    {
        float currentAlpha = text.color.a;
        float duration = 0.5f;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(currentAlpha, targetAlpha, timeElapsed / duration);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            yield return null;
        }

        text.color = new Color(text.color.r, text.color.g, text.color.b, targetAlpha);
    }

}
