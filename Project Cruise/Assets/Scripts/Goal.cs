using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    /////// PROPERTIES ///////
    public bool IsPressed { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        IsPressed = false;
        Invoke("AddGoal", 0.1f);
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            IsPressed = true;
            Blackboard.instance.LevelManager.CheckGoals();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsPressed = false;
        }
    }

    private void AddGoal()
    {
        Blackboard.instance.LevelManager.goals.Add(this);
    }
}
