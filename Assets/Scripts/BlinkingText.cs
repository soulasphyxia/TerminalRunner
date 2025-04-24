using TMPro;
using UnityEngine;

public class BlinkingText : MonoBehaviour
{
    [SerializeField] private TMP_Text textMeshPro; // ������ �� ��������� TextMeshPro
    public float blinkDuration = 2f; // ����� ������ ������� ����� ������� (� ��������)
    public bool isBlinking = true; // ���������/���������� ������� �������

    private Color originalColor; // �������� ���� ������
    private float timer = 0f;    // ������ ��� ������������ ��������� �������

    void Start()
    {
        if (textMeshPro != null)
        {
            originalColor = textMeshPro.color; // ��������� �������� ���� ������
        }
    }

    void Update()
    {
        if (!isBlinking || textMeshPro == null) return;

        // ��������� ������
        timer += Time.deltaTime;

        // ���� ������ ��������� ����������������� �����, ���������� ���
        if (timer > blinkDuration)
        {
            timer -= blinkDuration;
        }

        // ��������� ��������������� �������� � ��������� [0, 1] � �������������� ���������
        float normalizedTime = timer / blinkDuration; // ��������������� ����� � ��������� [0, 1]
        float alpha = (Mathf.Sin(normalizedTime * Mathf.PI * 2f) + 1f) * 0.5f; // ��������� � ��������� [0, 1]

        // ������������� ����� ���� � ���������� �������������
        textMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
    }

    /// <summary>
    /// ����� ��� ��������� ������� �������
    /// </summary>
    public void StartBlinking()
    {
        isBlinking = true;
        timer = 0f; // ���������� ������
    }

    /// <summary>
    /// ����� ��� ��������� ������� �������
    /// </summary>
    public void StopBlinking()
    {
        isBlinking = false;
        textMeshPro.color = originalColor; // ���������� �������� ����
    }
}
