using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    #region STRING CONSTANTS
    private string POST_LEVEL_SCENE_NAME = "PostLevel";
    #endregion

    private bool hasRelic;
    private bool hasWon;

    /////// PROPERTIES ///////
    public int TreasureCollected { get; set; }
    public bool RelicCollected { get; set; }
    public bool CharacterCanMove { get; private set; }

    public List<Goal> goals = new List<Goal>();
    public List<GameObject> playerOnGoal = new List<GameObject>();
    //public GameObject winCanvas;
    public GameObject loseCanvas;
    public GameObject pauseCanvas;
    public GameObject relicNotification;
    //Samuel 30 july 2020 - Add
    public GameObject inGameCanvas;
    public TMP_Text[] targetTime;
    public TMP_Text[] playerTimer;
    public TMP_Text triggeredTimerTime;
    public Image triggeredTimerFillImage;
    public float postLevelDelay = 2.0f;
    public bool hasAchievement = false;

    public int relicCoin = 30;

    private LevelTimer levelTimer;
    private LevelData currLevelData;
    public CheckPointSaveData checkPointSaveData;

    public bool noInteractables = false;

    private bool checkTolerance = true;

    // Awake is called before Start
    private void Awake()
    {
        Blackboard.Instance.LevelManager = this;

        levelTimer = GetComponent<LevelTimer>();
        //levelTimer.EnableTimer();                 enabled by LevelTimerEnabler.cs after first input
        checkPointSaveData = new CheckPointSaveData(levelTimer);
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
        TreasureCollected = 0;
        RelicCollected = false;
        CharacterCanMove = true;
        currLevelData = LevelDirectory.Instance.GetLevelData(GameData.Instance.currLevelID);
        foreach (var tmp in targetTime)
        {
            tmp.text = currLevelData.targetTime.ToString("0.00") + "s";
        }
        hasRelic = currLevelData.hasRelic;
        hasWon = false;
        //Debug.Log("CharacterCanMove set to true.");

        if (noInteractables)
        {
            Blackboard.Instance.Button.gameObject.SetActive(false);
        }
    }

    public void CheckGoals()
    {
        foreach (Goal goal in goals)
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

    public void EndLevelTimer()
    {
        levelTimer.EndTimer();
    }

    public void EndLevelTimer(bool isCheckingTolerance)
    {
        levelTimer.EndTimer(currLevelData.targetTime);
    }

    /// <summary>
    /// A method to handle player victory.
    /// </summary>
    private void Win()
    {
        //Samuel 30 july 2020 - Add
        SetInGameCanvas(false);

        //stopping triggered timer's coroutine to stop the timer
        if (Blackboard.Instance.TriggeredTimer) Blackboard.Instance.TriggeredTimer.StopTriggeredTimer();

        DisableCharacterMovement();
        EndLevelTimer(checkTolerance); //added argument to take overload function
        GameData.Instance.currTreasuresCollected = TreasureCollected;
        GameData.Instance.currIsRelicCollected = RelicCollected;
        CalculateCoinsAndStarsEarned();
        PlayGamesScript.Instance.SaveData();

        if (hasAchievement)
        {
            CheckAchievements();
        }

        //Debug.Log("Invoking Post Level");
        Invoke("LoadPostLevel", postLevelDelay);
    }
    /// <summary>
    /// A method to handle player defeat.
    /// </summary>
    public void Lose()
    {
        //Samuel 30 july 2020 - Add
        SetInGameCanvas(false);
        //Blackboard.Instance.MovementArrowManager.Direction = Vector2.zero;
        DisableCharacterMovement();
        levelTimer.EndTimer();
        Time.timeScale = 0.0f;
        playerTimer[0].text = GameData.Instance.currLevelTime.ToString("0.00") + "s";
        loseCanvas.SetActive(true);
    }

    /// <summary>
    /// A method to pause the game.
    /// </summary>
    /// <param name="pause">To pause or not to pause.</param>
    public void Pause(bool pause)
    {
        //Samuel 30 july 2020 - Add
        SetInGameCanvas(!pause);
        Time.timeScale = pause ? 0.0f : 1.0f;
        foreach (var tmp in playerTimer)
        {
            if (levelTimer.timer == 0f) tmp.text = "0.00s";
            else
            {
                tmp.text = levelTimer.timer.ToString("0.00") + "s";
            }
        }
    }

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
            // coinsEarned += relicCoin;                      //adds relic coin
            currLevelSave.hasFoundRelic = true;
            CheckRelicAchievement();
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
        // Hardcode for level 1 & 2 to be automatically completed
        if (GameData.Instance.currLevelID == 1 || GameData.Instance.currLevelID == 2) tempStar += 1;

        // if receive 3 stars and hasn't before
        if (tempStar == 3 && !currLevelSave.hasAchievedThreeStars)
        {
            // coinsEarned += currLevelData.threeStarsCoin;
            GameData.Instance.saveData.ThreeStarLevels++;
            CheckThreeStarAchievement();
            currLevelSave.hasAchievedThreeStars = true;
        }

        GameData.Instance.coinsEarned = coinsEarned;
        GameData.Instance.starsEarned = tempStar;
        GameData.Instance.saveData.Coins += coinsEarned;

        if (tempStar > currLevelSave.stars)
        {
            currLevelSave.stars = tempStar;
        }
    }

    public void SaveCheckpoint()
    {
        checkPointSaveData.SaveCheckPointSaveData();
    }

    public void LoadCheckpoint()
    {
        //Samuel 30 july 2020 - Add
        SetInGameCanvas(true);
        checkPointSaveData.LoadCheckPointSaveData();
        loseCanvas.SetActive(false);
        Time.timeScale = 1.0f;
        CharacterCanMove = true;
    }

    public void DisableCharacterMovement()
    {
        CharacterCanMove = false;
    }

    /// <summary>
    /// A method to handle checking for tier I and II achievements.
    /// </summary>
    private void CheckAchievements()
    {
        switch (GameData.Instance.currLevelID)
        {
            // basic
            case 3:
                PlayGamesScript.UnlockAchievement("CgkIscHpwIUZEAIQBA");
                break;
            // trap
            case 5:
                PlayGamesScript.UnlockAchievement("CgkIscHpwIUZEAIQBQ");
                break;
            // lever
            case 9:
                PlayGamesScript.UnlockAchievement("CgkIscHpwIUZEAIQBg");
                break;
            // finish stage 1
            case 20:
                PlayGamesScript.UnlockAchievement("CgkIscHpwIUZEAIQDQ");
                break;
            // key
            case 21:
                PlayGamesScript.UnlockAchievement("CgkIscHpwIUZEAIQBw");
                break;
            // pressure plate
            case 26:
                PlayGamesScript.UnlockAchievement("CgkIscHpwIUZEAIQCA");
                break;
            // unproportional
            case 33:
                PlayGamesScript.UnlockAchievement("CgkIscHpwIUZEAIQCQ");
                break;
            // finish stage 2
            case 40:
                PlayGamesScript.UnlockAchievement("CgkIscHpwIUZEAIQDg");
                break;
            // triggered timer
            case 41:
                PlayGamesScript.UnlockAchievement("CgkIscHpwIUZEAIQCg");
                break;
            // teleporter
            case 46:
                PlayGamesScript.UnlockAchievement("CgkIscHpwIUZEAIQCg");
                break;
            // finish stage 3
            case 60:
                PlayGamesScript.UnlockAchievement("CgkIscHpwIUZEAIQDw");
                break;
        }

        // collect first relic
        if (RelicCollected)
        {
            PlayGamesScript.UnlockAchievement("CgkIscHpwIUZEAIQDA");
        }
    }

    /// <summary>
    /// A method to handle checking for "Find All Relics" achievement.
    /// </summary>
    private void CheckRelicAchievement()
    {
        int[] relicLevels = {16, 23, 35, 40, 51};
        int relics = 0;

        for (int i = 0; i < relicLevels.Length; i++)
        {
            //added to check if the said level already has a levelSaveData
            if (GameData.Instance.saveData.levelSaveData.Count >= relicLevels[i])
            {
                if (GameData.Instance.saveData.levelSaveData[relicLevels[i] - 1].hasFoundRelic)
                {
                    //Debug.Log("Relic earned at level:" + GameData.Instance.saveData.levelSaveData[relicLevels[i]]);
                    relics++;
                }
            }
        }

        if (relics >= 5)
        {
            PlayGamesScript.UnlockAchievement("CgkIscHpwIUZEAIQEA");
        }
    }

    /// <summary>
    /// A method to handle checking for "Finish All Levels with 3 Stars" Achievement
    /// </summary>
    private void CheckThreeStarAchievement()
    {
        if (GameData.Instance.saveData.ThreeStarLevels >= 60)
        {
            PlayGamesScript.UnlockAchievement("CgkIscHpwIUZEAIQEQ");
        }
    }

    private void OnApplicationPause(bool pause)
    {
        pauseCanvas.SetActive(true);
        Pause(true);
    }

    /// <summary>
    /// A method to set In Game Canvas either On or Off depending on the parameter.
    /// </summary>
    public void SetInGameCanvas(bool status)
    {
        inGameCanvas.SetActive(status);
    }

}
