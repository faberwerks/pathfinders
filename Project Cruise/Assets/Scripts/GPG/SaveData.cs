using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int coins;
    public int lastLevelIndex;
    public List<LevelSaveData> levelSaveData;
    public DateTime timestamp;

    public SaveData()
    {
        coins = 0;
        lastLevelIndex = 0;
        levelSaveData = new List<LevelSaveData>();
        timestamp = DateTime.Now;
    }
}

public class LevelSaveData
{
    public float lowestTime = 0.0f;
    public bool hasFoundRelic = false;
    public int stars = 0;
}
