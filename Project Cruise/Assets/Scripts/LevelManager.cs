using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int treasureCollected;

    private bool hasRelic,
                 relicCollected;

    private float levelTimer;

    //Awake is called before Start
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    private void Start()
    {
        Blackboard.instance.LevelManager = this;
        treasureCollected = 0;
        relicCollected = false;
    }

    /////// PROPERTIES ///////
    public int TreasureCollected
    {
        get
        {
            return treasureCollected;
        }
        set
        {
            treasureCollected += value;
        }
    }

    /////// PROPERTIES ///////
    public bool RelicCollected
    {
        get
        {
            return relicCollected;
        }
        set
        {
            relicCollected = value;
        }
    }


    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
