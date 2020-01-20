using TMPro;
using UnityEngine;

public class PostLevelManager : MonoBehaviour
{
    private string[] RATING = new string[3] { "Nice!", "Great!", "Perfect!" };
    public TextMeshProUGUI rating;
    public TextMeshProUGUI targetTimeText;
    public TextMeshProUGUI playerTimeText;
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
        Time.timeScale = 1.0f;
        treasure = GameData.Instance.currTreasuresCollected;
        relicCanvas.SetActive(GameData.Instance.currIsRelicCollected ? true : false);
        treasureResult.text = treasure + "/3";
        targetTime = LevelDirectory.Instance.GetLevelData(GameData.Instance.currLevelID).targetTime;
        playerTime = GameData.Instance.currLevelTime;
        ShowTimes();

        ShowResult(GameData.Instance.starsEarned);

        coinsEarnedText.text = "" + GameData.Instance.coinsEarned;
        GameData.Instance.interstitialAdsCounter -= 1;
        Debug.Log(GameData.Instance.interstitialAdsCounter);
        AdsHandler.instance.ShowInterstitialAD();
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
            icon.SetActive(true);
        }
    }

    private void ShowTimes()
    {
        targetTimeText.text = targetTime.ToString("0.00") + "s";
        playerTimeText.text = playerTime.ToString("0.00") + "s";
    }
}
