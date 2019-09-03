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
    public bool RelicCollected { get; set; }
    //check when button is pressed
    public bool IsInteracting { get; set; }

    //Public list of goals
    public List<Goal> goals;

    //Awake is called before Start
    private void Awake()
    {
        Blackboard.instance.LevelManager = this;
    }

    private void Start()
    {
       
        TreasureCollected = 0;
        RelicCollected = false;
        IsInteracting = false;
    }

    public void CheckGoals()
    {
        foreach(Goal goal in goals)
        {
            if (goal.IsPressed == false)
                return;
        }
        Win();
    }

    private void Win()
    {
            Debug.Log("win");
    }

    public void Lose()
    {
        Debug.Log("lose");
    }
    

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
