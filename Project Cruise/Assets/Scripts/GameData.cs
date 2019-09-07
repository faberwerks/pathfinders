using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    private static GameData instance;
    public static GameData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameData();
            }
            return instance;
        }
    }

    public int treasuresCollected = 0;
    public bool isRelicCollected = false;
    public float levelTime = 0.0f;
    public int lastSceneBuildIndex = 0;
}
