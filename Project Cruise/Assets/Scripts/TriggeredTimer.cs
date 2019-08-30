using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Script to manage triggered time and what path it opens when preasure plate is pressed
/// </summary>
public class TriggeredTimer : MonoBehaviour
{
    public GameObject path;

    public float countdownTime;

    public bool timeActive;
    
    // Update is called once per frame
    void Update()
    {
        if(timeActive)
        {
            countdownTime -= Time.deltaTime;
            if(countdownTime <= 0)
            {
                Blackboard.instance.LevelManager.Lose();
            }
        }
    }

    //Activate time and open path to escape
    public void IsActivating()
    {
        if(path != null)
        Destroy(path);
        timeActive = true;
    }
}
