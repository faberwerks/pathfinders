using TMPro;
using UnityEngine;

public class PostLevelManager : MonoBehaviour
{
    #region STRING CONSTANTS
    private const string TREASURE_RESULT = "/3";
    private const string TARGET_TIME = "Target: ";
    private const string PLAYER_TIME = "Your time: ";
    private const string RATING_0 = "Not bad!";
    private const string RATING_1 = "Nice!";
    private const string RATING_2 = "Great!";
    private const string RATING_3 = "Perfect!";
    private const string TIME_PASS = "Great job!\nFaster than the light!";
    private const string TIME_FAIL = "Better luck next time!";
    #endregion

    public TextMeshProUGUI rating;
    public TextMeshProUGUI targetTime;
    public TextMeshProUGUI playerTime;
    public TextMeshProUGUI timeResult;
    public TextMeshProUGUI treasureResult;
    public TextMeshProUGUI collectedIGC;

    public GameObject[] treasureIcon;
    public GameObject relicCanvas;

    // Start is called before the first frame update
    void Start()
    {
        short _treasuresCollected = (short)GameData.Instance.treasuresCollected;
        treasureResult.text = _treasuresCollected + TREASURE_RESULT;
        ActivateTreasureIcon(_treasuresCollected);
        switch (_treasuresCollected)
        {
            case 0:
                rating.text = RATING_0;
                break;
            case 1:
                rating.text = RATING_1;
                break;
            case 2:
                rating.text = RATING_2;
                break;
            case 3:
                rating.text = RATING_3;
                break;
        }

        targetTime.text = TARGET_TIME + LevelDirectory.Instance.GetLevelData(GameData.Instance.lastLevelIndex).targetTime + "s";
        playerTime.text = PLAYER_TIME + GameData.Instance.levelTime.ToString("#.##") + "s";
        //TO DO: Compare target time with player time
        //       then show the result
        if (GameData.Instance.levelTime <= LevelDirectory.Instance.GetLevelData(GameData.Instance.lastLevelIndex).targetTime)
        {
            timeResult.text = TIME_PASS;
        }
        else
        {
            timeResult.text = TIME_FAIL;
        }

        relicCanvas.SetActive(GameData.Instance.isRelicCollected ? true : false);

    }

    /// <summary>
    /// Activates treasure icons based on the number of collected treasures
    /// </summary>
    /// <param name="_treasure">Number of collected treasure</param>
    private void ActivateTreasureIcon(short _treasure)
    {
        foreach (GameObject icon in treasureIcon)
        {
            if (_treasure-- <= 0) break;
            //Debug.Log("set icon");
            icon.SetActive(true);
        }
    }

}
