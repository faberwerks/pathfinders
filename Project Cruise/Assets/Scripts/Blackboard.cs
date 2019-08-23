using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script to keep all the important references in one place
/// </summary>
public class Blackboard : MonoBehaviour
{
    //private Joystick joystick;
    //private LevelManager levelManager;

    public static Blackboard instance;

    /////// PROPERTIES ///////
    public Joystick Joystick { get; set; }
    public LevelManager LevelManager { get; set; }

    private void Awake()
    {
        instance = new Blackboard();
    }

    

    
    
}
