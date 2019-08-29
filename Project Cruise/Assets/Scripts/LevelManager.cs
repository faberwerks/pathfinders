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
    //Samuel 29 August 2019 - Add
    public int TotalGoal { get; set; }
    public int Goal { get; set; }
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
        //Samuel 29 August 2019 - Add
        TotalGoal = 0;
        Goal = 0;
    }

    public void CheckGoals()
    {
    //Commented By Samuel 29 August 2019 - Change Algorithm
//         foreach(Goal goal in goals)
//         {
//             if (goal.IsPressed == false)
//                 return;
//         }
          if (TotalGoal != Goal)
                return;
        Win();
    }

    private void Win()
    {
        
            Debug.Log("win");
    }
    

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
