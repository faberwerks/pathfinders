using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script to define what the goal must do 
/// </summary>
public class Goal : MonoBehaviour
{
    /////// PROPERTIES ///////
    public bool IsPressed { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        IsPressed = false;
        Blackboard.instance.LevelManager.goals.Add(this);
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    //Method CheckGoals is called here so there is no need for this method called in update function 
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
}
