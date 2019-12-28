using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatHandler : MonoBehaviour
{
    public void UnlockAllLevels()
    {
        GameData.Instance.saveData.LastLevelNumber = 60;
    }
}
