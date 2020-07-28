using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A script to keep all the important references in one place
/// </summary>
public class Blackboard
{
    private static Blackboard instance;
    public static Blackboard Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Blackboard();
            }
            return instance;
        }
    }

    /////// PROPERTIES ///////
    //public Joystick Joystick { get; set; }
    public MovementArrowManager movementArrowManager { get; set; }
    public LevelManager LevelManager { get; set; }
    public Button Button { get; set; }
    public int CurrentLevelIndex { get; set;}
    public TriggeredTimer TriggeredTimer { get; set; }
}
