using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region STRING CONSTANTS
    private string POST_LEVEL_SCENE_NAME = "PostLevel";
    #endregion

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
    public GameObject winCanvas;
    public float postLevelDelay = 2.0f;

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

    /// <summary>
    /// A method to handle player victory.
    /// </summary>
    private void Win()
    {
        GameData.Instance.treasuresCollected = TreasureCollected;
        GameData.Instance.isRelicCollected = RelicCollected;
        //GameData.Instance.levelTime
        GameData.Instance.lastSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        Invoke("LoadPostLevel", postLevelDelay);
    }

    public void Lose()
    {
        Debug.Log("lose");
        //TO-DO: add LOSE PANEL
    }
    
    /// <summary>
    /// A method to pause the game.
    /// </summary>
    /// <param name="pause">To pause or not to pause.</param>
    public void Pause(bool pause)
    {
        Time.timeScale = pause ? 0.0f : 1.0f;
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    /// <summary>
    /// A method to load the Post Level scene.
    /// </summary>
    private void LoadPostLevel()
    {
        SceneManager.LoadScene(POST_LEVEL_SCENE_NAME);
    }
}
