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

    //Public list of goals
    public List<Goal> goals = new List<Goal>();

    //Awake is called before Start
    //private void Awake()
    //{

    //}

    // Start is called before the first frame update
    private void Start()
    {
        Blackboard.instance.LevelManager = this;
        TreasureCollected = 0;
        RelicCollected = false;
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
    
    /// <summary>
    /// A method to handle losing a level.
    /// </summary>
    public void Lose()
    {
        Debug.Log("Lose");
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
