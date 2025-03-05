using UnityEngine;

public static class GameEvents
{
    public static event System.Action<GameObject> OnEnemyDestroyed;
    public static event System.Action OnEnemyGetAway;
    public static event System.Action GameOver;

    public static void CallOnEnemyDestroyed(GameObject explosionPrefab)
    {
        OnEnemyDestroyed?.Invoke(explosionPrefab);
    }

    public static void CallOnEnemyGetAway() {
        OnEnemyGetAway?.Invoke(); 
    }

    public static void CallGameOver()
    {
        GameOver?.Invoke();
    }
}
