using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Scriptable Object holding references to all level metadata.
/// </summary>
[CreateAssetMenu(menuName = "Tools/Level Directory")]
public class LevelDirectory : ScriptableObject
{
    private static LevelDirectory instance = null;

    public static LevelDirectory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<LevelDirectory>("Level Directory");
            }

            return instance;
        }
    }

    [HideInInspector]
    public List<LevelData> levels;

    /// <summary>
    /// A method to get the level data at the specified index.
    /// </summary>
    /// <param name="index">Level index.</param>
    /// <returns>Level data.</returns>
    public LevelData GetLevelData(int index)
    {
        return levels[index - 1];
    }
}
