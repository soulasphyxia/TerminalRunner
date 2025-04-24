using UnityEngine;
using UnityEngine.SceneManagement;

public static class DontDestroyCleaner
{
    public static void DestroyAllDontDestroyOnLoad()
    {
        GameObject temp = new GameObject("Temp");
        Object.DontDestroyOnLoad(temp);
        Scene ddolScene = temp.scene;
        Object.Destroy(temp);

        foreach (GameObject go in ddolScene.GetRootGameObjects())
        {
            Object.Destroy(go);
        }
    }
}