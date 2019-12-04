using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnlockAllLevels()
    {
        GameData.Instance.saveData.LastLevelNumber = 60;
    }
}
