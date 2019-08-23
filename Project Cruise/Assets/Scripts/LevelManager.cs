using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //private int treasureCollected;

    private bool hasRelic;

    //private bool relicCollected;

    private float levelTimer;

    /////// PROPERTIES ///////
    public int TreasureCollected { get; set; }

    /////// PROPERTIES ///////
    public bool RelicCollected { get; set; }

    //Awake is called before Start
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    private void Start()
    {
        Blackboard.instance.LevelManager = this;
        TreasureCollected = 0;
        RelicCollected = false;
    }

    

    

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
