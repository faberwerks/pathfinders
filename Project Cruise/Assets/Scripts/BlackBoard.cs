using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoard : MonoBehaviour
{
    private Joystick joystick;
    private LevelManager levelManager;

    public static BlackBoard instance;

    private void Awake()
    {
        instance = new BlackBoard();
    }

    /////// PROPERTIES ///////
    public Joystick Joystick
    {
        get
        {
            return joystick;
        }
        set
        {
            if(joystick == null)
                joystick = value;
        }
    }

    /////// PROPERTIES ///////
    public LevelManager LevelManager
    {
        get
        {
            return levelManager;
        }
        set
        {
            if (levelManager == null)
                levelManager = value;
        }
    }
    
}
