using UnityEngine;

public class LevelData : ScriptableObject
{
    [SerializeField] public string levelID = "";
    [SerializeField] private float targetTime = 0.0f;
    [SerializeField] private bool hasRelic = false;
}
