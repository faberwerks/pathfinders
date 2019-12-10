using TMPro;
using UnityEngine;

public class PostLevelManager : MonoBehaviour
{
    //#region STRING CONSTANTS
    ////private const string TREASURE_RESULT = "/3";
    ////private const string TARGET_TIME = "Target: ";
    ////private const string PLAYER_TIME = "Your time: ";
    ////private const string RATING_0 = "Not bad!";
    //private const string RATING_1 = "Nice!";
    //private const string RATING_2 = "Great!";
    //private const string RATING_3 = "Perfect!";
    ////private const string TIME_PASS = "Great job!\nFaster than the light!";
    ////private const string TIME_FAIL = "Better luck next time!";
    //#endregion


    private string[] RATING = new string[3] { "Nice!", "Great!", "Perfect!" };
    public TextMeshProUGUI rating;
    public TextMeshProUGUI targetTimeText;
    public TextMeshProUGUI playerTimeText;
    //public TextMeshProUGUI timeResult;
    public TextMeshProUGUI treasureResult;
    public TextMeshProUGUI coinsEarnedText;

    public GameObject[] treasureIcon;
    public GameObject relicCanvas;

    private float targetTime = 0f;
    private float playerTime = 0f;
    private bool passedTarget = false;
    private int treasure = 0;

    // Start is called before the first frame update
    void Start()
    {
        #region OLD ALGORITHM
        //short _treasuresCollected = (short)GameData.Instance.treasuresCollected;
        //treasureResult.text = _treasuresCollected + TREASURE_RESULT;
        //ActivateTreasureIcon(_treasuresCollected);
        //switch (_treasuresCollected)
        //{
        //    case 0:
        //        rating.text = RATING_0;
        //        break;
        //    case 1:
        //        rating.text = RATING_1;
        //        break;
        //    case 2:
        //        rating.text = RATING_2;
        //        break;
        //    case 3:
        //        rating.text = RATING_3;
        //        break;
        //}
        //targetTime.text = TARGET_TIME + LevelDirectory.Instance.GetLevelData(GameData.Instance.lastLevelIndex).targetTime + "s";
        //playerTime.text = PLAYER_TIME + GameData.Instance.levelTime.ToString("#.##") + "s";
        ////TO DO: Compare target time with player time
        ////       then show the result
        //if (GameData.Instance.levelTime <= LevelDirectory.Instance.GetLevelData(GameData.Instance.lastLevelIndex).targetTime)
        //{
        //    timeResult.text = TIME_PASS;
        //}
        //else
        //{
        //    timeResult.text = TIME_FAIL;
        //}

        //relicCanvas.SetActive(GameData.Instance.isRelicCollected ? true : false);
        #endregion

        treasure = GameData.Instance.currTreasuresCollected;
        treasureResult.text = treasure + "/3";
        targetTime = LevelDirectory.Instance.GetLevelData(GameData.Instance.currLevelID).targetTime;
        playerTime = GameData.Instance.currLevelTime;
        //change the floats into 2 decimal places
        targetTime = Mathf.Round(targetTime * 100f) / 100f;
        playerTime = Mathf.Round(playerTime * 100f) / 100f;
        ShowTimes();
        //if (playerTime <= targetTime) passedTarget = true;

        ShowResult(GameData.Instance.starsEarned);

        coinsEarnedText.text = "" + GameData.Instance.coinsEarned;

    }

    private void ShowResult(int stars)
    {
        ActivateStarIcon(stars);
        if (stars <= 0)
        {
            rating.text = RATING[0];
        }
        else
        {
            rating.text = RATING[stars - 1];
        }
    }

    private void ActivateStarIcon(int _stars)
    {
        foreach (GameObject icon in treasureIcon)
        {
            if (_stars -- <= 0) break;
            //Debug.Log("set icon");
            icon.SetActive(true);
        }
    }

    private void ShowTimes()
    {
        targetTimeText.text = targetTime.ToString("#.##") + "s";
        playerTimeText.text = playerTime.ToString("#.##") + "s";
    }
}
