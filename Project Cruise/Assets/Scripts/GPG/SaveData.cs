using System;
using System.Collections.Generic;

/// <summary>
/// A class to hold save data.
/// </summary>
[System.Serializable]
public class SaveData
{
    public int Coins { get; set; }
    public int LastLevelIndex { get; set; }
    public DateTime Timestamp { get; private set; }
    public List<LevelSaveData> levelSaveData;

    public SaveData()
    {
        Coins = 0;
        LastLevelIndex = 0;
        levelSaveData = new List<LevelSaveData>();
        UpdateTimestamp();
    }

    /// <summary>
    /// Updates the timestamp.
    /// </summary>
    public void UpdateTimestamp()
    {
        Timestamp = DateTime.Now;
    }
}

/// <summary>
/// A class to hold save data for a level.
/// </summary>
public class LevelSaveData
{
    public float lowestTime = 0.0f;
    public bool hasFoundRelic = false;
    public int stars = 0;
}
