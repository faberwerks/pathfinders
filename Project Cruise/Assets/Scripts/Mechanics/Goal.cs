﻿using System.Collections;
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
        Blackboard.Instance.LevelManager.goals.Add(this);
        //Blackboard.instance.LevelManager.goals.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        //if (!Blackboard.instance.LevelManager.goals.Contains(this))
        //{
        //    Blackboard.instance.LevelManager.goals.Add(this);
        //}
    }

    //Method CheckGoals is called here so there is no need for this method called in update function 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(TagStrings.PLAYER_TAG))
        {
            IsPressed = true;
            Blackboard.Instance.LevelManager.CheckGoals();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagStrings.PLAYER_TAG))
        {
            IsPressed = false;
        }
    }
}