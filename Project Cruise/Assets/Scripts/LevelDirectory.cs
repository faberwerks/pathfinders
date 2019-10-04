using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Tools/Hi")]
public class LevelDirectory : ScriptableObject
{
    private static LevelDirectory instance = null;

    public static LevelDirectory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<LevelDirectory>("New Level Directory");
            }

            return instance;
        }
    }

    public LevelData[] levelDirectory;
    public string mamen;

    public string GetLevelID(int index)
    {
        return levelDirectory[index - 1].levelID;
    }
}
