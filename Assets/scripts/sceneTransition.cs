using UnityEngine.SceneManagement;
using UnityEngine;

public class sceneTransition : MonoBehaviour
{
    public int SceneNumber;

    public void Transition()
    {
        SceneManager.LoadScene(SceneNumber);
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        DontDestroyCleaner.DestroyAllDontDestroyOnLoad();

        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}