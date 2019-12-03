using UnityEngine;

/// <summary>
/// A Scriptable Object to hold level metadata.
/// </summary>
[CreateAssetMenu(menuName = "Tools/Level Data")]
public class LevelData : ScriptableObject
{
    public string levelID = "";
    public float targetTime = 0.0f;
    public bool hasRelic = false;
    public int baseCoin = 0;
    public int treasureCoin = 0;
    public int targetTimeCoin = 0;
}
