using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JsonLoader : MonoBehaviour
{
    [SerializeField] private string jsonFileName = "commands";
    private string mainMenuSceneName = "Menu"; // Название сцены главного меню

    // Синглтон-экземпляр
    public static JsonLoader Instance { get; private set; }

    // Список объектов, созданных из JSON
    private List<JsonCommand> commands = new List<JsonCommand>();

    // Публичное свойство для доступа к командам из других классов
    public List<JsonCommand> Commands => commands;

    private void Awake()
    {
        // Инициализация синглтона
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Делаем объект неуничтожаемым при переходе между сценами
        }
        else
        {
            Destroy(gameObject); // Уничтожаем дублирующий объект, если он уже существует
        }
    }

    private void Start()
    {
        // Загружаем и парсим JSON
        LoadAndParseJson();

        // После завершения загрузки переходим на сцену главного меню
        LoadMainMenu();
    }

    private void LoadAndParseJson()
    {
        // Загрузка JSON-файла из папки Resources
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonFileName);

        if (jsonFile == null)
        {
            Debug.LogError($"JSON file '{jsonFileName}' not found in Resources folder.");
            return;
        }

        try
        {
            // Парсинг JSON с использованием JsonUtility
            CommandsWrapper wrapper = JsonUtility.FromJson<CommandsWrapper>(jsonFile.text);
            commands = wrapper.commands;

            Debug.Log($"Successfully loaded {commands.Count} commands from JSON.");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to parse JSON: {e.Message}");
        }
    }

    // Метод для загрузки главного меню
    private void LoadMainMenu()
    {
        Debug.Log("Loading main menu scene...");
        SceneManager.LoadScene(mainMenuSceneName);
    }

    // Вспомогательный класс-обертка для массива объектов в JSON
    [System.Serializable]
    private class CommandsWrapper
    {
        public List<JsonCommand> commands;
    }
}