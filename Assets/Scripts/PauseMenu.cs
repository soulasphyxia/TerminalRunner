using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu; // Ссылка на панель паузы

    private bool isPaused = false;

    private void Update()
    {
        // Проверяем нажатие Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true); // Показываем меню
        Time.timeScale = 0f; // Останавливаем время
        AudioListener.pause = true; // Приостанавливаем звуки
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false); // Скрываем меню
        Time.timeScale = 1f; // Возобновляем время
        AudioListener.pause = false; // Включаем звуки
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f; // Сбрасываем паузу
        SceneManager.LoadScene("Menu"); // Загружаем меню
    }

    public void QuitGame()
    {
        Application.Quit(); // Закрываем игру
    }
}