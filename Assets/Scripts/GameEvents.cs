using UnityEngine;

public static class GameEvents
{
    public static event System.Action OnEnemyDestroyed;
    public static event System.Action OnEnemyGetAway;


    public static void CallOnEnemyDestroyed()
    {
        OnEnemyDestroyed?.Invoke();
    }

    public static void CallOnEnemyGetAway() {
        OnEnemyGetAway?.Invoke(); 
    }
}
