using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JsonLoader : MonoBehaviour
{
    [SerializeField] private string jsonFileName = "commands";
    private string mainMenuSceneName = "Menu"; // �������� ����� �������� ����

    // ��������-���������
    public static JsonLoader Instance { get; private set; }

    // ������ ��������, ��������� �� JSON
    private List<JsonCommand> commands = new List<JsonCommand>();

    // ��������� �������� ��� ������� � �������� �� ������ �������
    public List<JsonCommand> Commands => commands;

    private void Awake()
    {
        // ������������� ���������
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ������ ������ �������������� ��� �������� ����� �������
        }
        else
        {
            Destroy(gameObject); // ���������� ����������� ������, ���� �� ��� ����������
        }
    }

    private void Start()
    {
        // ��������� � ������ JSON
        LoadAndParseJson();

        // ����� ���������� �������� ��������� �� ����� �������� ����
        LoadMainMenu();
    }

    private void LoadAndParseJson()
    {
        // �������� JSON-����� �� ����� Resources
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonFileName);

        if (jsonFile == null)
        {
            Debug.LogError($"JSON file '{jsonFileName}' not found in Resources folder.");
            return;
        }

        try
        {
            // ������� JSON � �������������� JsonUtility
            CommandsWrapper wrapper = JsonUtility.FromJson<CommandsWrapper>(jsonFile.text);
            commands = wrapper.commands;

            Debug.Log($"Successfully loaded {commands.Count} commands from JSON.");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to parse JSON: {e.Message}");
        }
    }

    // ����� ��� �������� �������� ����
    private void LoadMainMenu()
    {
        Debug.Log("Loading main menu scene...");
        SceneManager.LoadScene(mainMenuSceneName);
    }

    // ��������������� �����-������� ��� ������� �������� � JSON
    [System.Serializable]
    private class CommandsWrapper
    {
        public List<JsonCommand> commands;
    }
}