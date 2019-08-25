using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Collectible
{
    //It add point to the treasure collected
    protected override void OnCollected()
    {
        Blackboard.instance.LevelManager.TreasureCollected += 1;
        Debug.Log(Blackboard.instance.LevelManager.TreasureCollected);
    }
}
