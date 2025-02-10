using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneTransition : MonoBehaviour
{
    public int SceneNumber;

    public void Transition()
    {
        SceneManager.LoadScene(SceneNumber);
    }

}
