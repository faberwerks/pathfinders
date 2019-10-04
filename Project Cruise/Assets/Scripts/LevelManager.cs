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
    //private float levelTimer;

    /////// PROPERTIES ///////
    public int TreasureCollected { get; set; }
    public bool RelicCollected { get; set; }

    //Public list of goals
    public List<Goal> goals = new List<Goal>();
    public GameObject winCanvas;
    public GameObject loseCanvas;
    public float postLevelDelay = 2.0f;

    private LevelTimer levelTimer;

    //Awake is called before Start
    private void Awake()
    {
        Blackboard.Instance.LevelManager = this;

        levelTimer = GetComponent<LevelTimer>();
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
        TreasureCollected = 0;
        RelicCollected = false;
    }

    private void Update()
    {
        //if (Blackboard.instance.LevelManager == null)
        //{
        //    Blackboard.instance.LevelManager = this;
        //}
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
        levelTimer.EndTimer();
        GameData.Instance.treasuresCollected = TreasureCollected;
        GameData.Instance.isRelicCollected = RelicCollected;
        //GameData.Instance.levelTime
        GameData.Instance.lastSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        Invoke("LoadPostLevel", postLevelDelay);
    }
    /// <summary>
    /// A method to handle player defeat.
    /// </summary>
    public void Lose()
    {
        //Debug.Log("lose");
        loseCanvas.SetActive(true);
        Time.timeScale = 0.0f;
    }
    
    /// <summary>
    /// A method to pause the game.
    /// </summary>
    /// <param name="pause">To pause or not to pause.</param>
    public void Pause(bool pause)
    {
        Time.timeScale = pause ? 0.0f : 1.0f;
    }

    //// update is called once per frame
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
