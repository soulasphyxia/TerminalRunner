using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneTransition : MonoBehaviour
{
    public int SceneNumber;

    public void Transition()
    {
        SceneManager.LoadScene(SceneNumber);
    }

    public void QuitApplication()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stops Play Mode in Editor
        #else
            Application.Quit(); // Works in builds (Windows, Mac, Linux, etc.)
        #endif
    }


}
