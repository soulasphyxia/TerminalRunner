using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour, IStageable
{
    [SerializeField] private int _number;
    [SerializeField] private float _spawnRate;
    [SerializeField] private float _speed;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int[] _diffs;
    [SerializeField] bool _isLastStage;
    [SerializeField] private int _enemiesToKill;

    public int EnemiesToKill()
    {
        return _enemiesToKill;
    }

    public Sprite GetBackground()
    {
        return _sprite;
    }

    public int[] GetDifficulties()
    {
        return _diffs;  
    }

    public float GetSpawnRate()
    {
        return _spawnRate;
    }

    public float GetSpeed()
    {
        return _speed;
    }

    public int GetStageNumber()
    {
       return _number;
    }

    public bool IsLastStage()
    {
        return _isLastStage;
    }
}
