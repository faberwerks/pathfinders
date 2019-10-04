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
                instance = Resources.Load<LevelDirectory>("New Level Directory");
            }

            return instance;
        }
    }

    public LevelData[] levelDirectory;

    /// <summary>
    /// A method to get the level data at the specified index.
    /// </summary>
    /// <param name="index">Level index.</param>
    /// <returns>Level data.</returns>
    public LevelData GetLevelData(int index)
    {
        return levelDirectory[index - 1];
    }
}
