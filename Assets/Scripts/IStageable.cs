using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public interface IStageable
{
    int GetStageNumber();
    float GetSpawnRate();
    float GetSpeed();
    Sprite GetBackground();

    int EnemiesToKill();
    bool IsLastStage();

    int[] GetDifficulties();
}
