using UnityEngine;

public static class GameEvents
{
    public static event System.Action OnEnemyDestroyed;
    public static event System.Action OnEnemyGetAway;
    public static event System.Action GameOver;
    public static event System.Action<IStageable> OnChangeStage;
    public static event System.Action NotLasStage;
    public static event System.Action AddCombo;
    public static event System.Action ResetCombo;

    public static void CallOnEnemyDestroyed()
    {
        OnEnemyDestroyed?.Invoke();
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

    public static void CallAddCombo()
    {
        AddCombo?.Invoke();
    }

    public static void CallResetCombo()
    {
        ResetCombo?.Invoke();
    }

}
