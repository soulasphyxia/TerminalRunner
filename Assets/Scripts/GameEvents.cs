using UnityEngine;

public static class GameEvents
{
    public static event System.Action<GameObject> OnEnemyDestroyed;
    public static event System.Action OnEnemyGetAway;
    public static event System.Action GameOver;
    public static event System.Action<IStageable> OnChangeStage;
    public static event System.Action NotLasStage;

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

    public static void CallChangeStage(IStageable stage) { 
    
        OnChangeStage?.Invoke(stage);
    }

    public static void CallNotLastStage()
    {
        NotLasStage?.Invoke();
    }

}
