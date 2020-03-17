using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    #region STRING CONSTANTS
    private string POST_LEVEL_SCENE_NAME = "PostLevel";
    #endregion

    //private int treasureCollected;
    private bool hasRelic;
    //private bool relicCollected;
    //private float levelTimer;
    private bool hasWon;

    /////// PROPERTIES ///////
    public int TreasureCollected { get; set; }
    public bool RelicCollected { get; set; }

    //Public list of goals
    public List<Goal> goals = new List<Goal>();
    public GameObject winCanvas;
    public GameObject loseCanvas;
    public TMP_Text[] targetTime;
    public TMP_Text playerTimer;
    public float postLevelDelay = 2.0f;

    public int relicCoin = 30;

    private LevelTimer levelTimer;
    private LevelData currLevelData;

    public bool noInteractables = false;

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
        currLevelData = LevelDirectory.Instance.GetLevelData(GameData.Instance.currLevelID);
        foreach (var tmp in targetTime)
        {
            tmp.text = currLevelData.targetTime.ToString("0.00") + "s";
        }
        //targetTime.text = "Target time: " + currLevelData.targetTime;
        hasRelic = currLevelData.hasRelic;
        hasWon = false;

        if (noInteractables)
        {
            Blackboard.Instance.Button.gameObject.SetActive(false);
        }
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
        if (!hasWon)
        {
            hasWon = true;
            Win();
        }
    }

    /// <summary>
    /// A method to handle player victory.
    /// </summary>
    private void Win()
    {
        levelTimer.EndTimer();
        GameData.Instance.currTreasuresCollected = TreasureCollected;
        GameData.Instance.currIsRelicCollected = RelicCollected;
        //GameData.Instance.levelTime
        //GameData.Instance.lastSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        CalculateCoinsAndStarsEarned();
        GameData.Instance.saveData.UpdateTimestamp();
        PlayGamesScript.Instance.SaveData();
       
        Debug.Log("Invoking Post Level");
        Invoke("LoadPostLevel", postLevelDelay);
        //Time.timeScale = 0.0f;
    }
    /// <summary>
    /// A method to handle player defeat.
    /// </summary>
    public void Lose()
    {
        levelTimer.EndTimer();
        Time.timeScale = 0.0f;
        playerTimer.text = GameData.Instance.currLevelTime.ToString("#.##") + "s";
        loseCanvas.SetActive(true);
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

    private void CalculateCoinsAndStarsEarned()
    {
        int coinsEarned = 0;
        int tempStar = 1;
        // bool firstTimeThisLevel = false;

        SaveData.LevelSaveData currLevelSave = null;

        // if current level is latest level
        // use existing latest level save data
        if (GameData.Instance.saveData.LastLevelNumber == GameData.Instance.currLevelID)
        {
            currLevelSave = GameData.Instance.saveData.levelSaveData[GameData.Instance.saveData.levelSaveData.Count - 1];
            // add new level save data for next level
            GameData.Instance.saveData.levelSaveData.Add(new SaveData.LevelSaveData());
            // update last level number to latest level (next level)
            GameData.Instance.saveData.LastLevelNumber += 1;

            // firstTimeThisLevel = true;
        }
        // else
        // use appropriate level save data
        else
        {
            currLevelSave = GameData.Instance.saveData.levelSaveData[GameData.Instance.currLevelID - 1];
        }

        coinsEarned += currLevelData.baseCoin;             //adds base coin

        // if there is a relic
        // and relic is collected
        // and hasn't found relic before
        if (hasRelic && RelicCollected && !currLevelSave.hasFoundRelic)
        {
            coinsEarned += relicCoin;                      //adds relic coin
            currLevelSave.hasFoundRelic = true;
        }
        
        // if all treasures collected
        if (TreasureCollected >= 3)
        {
            tempStar += 1;

            // if hasn't collected treasures before
            if (!currLevelSave.hasCollectedTreasures)
            {
                coinsEarned += currLevelData.treasureCoin;     //adds treasure coin
                currLevelSave.hasCollectedTreasures = true;
            }
        }
        //Hardcode for level 1 to be automatically completed
        if (GameData.Instance.currLevelID == 1) tempStar += 1;

        // if finished within target time
        if (levelTimer.timer <= currLevelData.targetTime)
        {
            tempStar += 1;

            // if hasn't achieved target time before
            if (!currLevelSave.hasAchievedTargetTime)
            {
                coinsEarned += currLevelData.targetTimeCoin;   //adds target time coin
             
                currLevelSave.hasAchievedTargetTime = true;
            }
        }
        //Hardcode for level 1 & 2 to be automatically completed
        if (GameData.Instance.currLevelID == 1 || GameData.Instance.currLevelID == 2) tempStar += 1;

        // if receive 3 stars and hasn't before
        if (tempStar == 3 && !currLevelSave.hasAchievedThreeStars)
        {
            coinsEarned += currLevelData.threeStarsCoin;
            currLevelSave.hasAchievedThreeStars = true;
        }

        GameData.Instance.coinsEarned = coinsEarned;
        GameData.Instance.starsEarned = tempStar;
        GameData.Instance.saveData.Coins += coinsEarned;

        //if (firstTimeThisLevel)
        //{
        //    GameData.Instance.saveData.Stars += tempStar;
        //}
        //else
        //{
        //    if (currLevelSave.stars < tempStar)
        //    {
        //        GameData.Instance.saveData.Stars += (tempStar - currLevelSave.stars);
        //    }
        //}

        if (tempStar > currLevelSave.stars)
        {
            currLevelSave.stars = tempStar;
        }

        GameData.Instance.saveData.UpdateTimestamp();
    }
}
