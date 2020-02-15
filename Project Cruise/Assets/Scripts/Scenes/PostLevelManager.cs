using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PostLevelManager : MonoBehaviour
{
    public Sprite[] ratings;
    // public TextMeshProUGUI rating;
    public Image ratingImage;
    public TextMeshProUGUI targetTimeText;
    public TextMeshProUGUI playerTimeText;
    public TextMeshProUGUI treasureResult;
    public TextMeshProUGUI coinsEarnedText;

    public GameObject[] starIcon;
    public GameObject relicCanvas;

    private float targetTime = 0f;
    private float playerTime = 0f;
    private bool passedTarget = false;
    private int treasure = 0;

    //To set things unactive in Level 1 and 2
    public TextMeshProUGUI targetTimeLabelText;
    public TextMeshProUGUI playerTimeLabelText;
    public Image treasureImage;

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

        if (GameData.Instance.currLevelID <= 2)
        {
            if (GameData.Instance.currLevelID == 1)
            {
                treasureImage.gameObject.SetActive(false);
                treasureResult.text = "";
            }
            targetTimeLabelText.text = "";
            playerTimeLabelText.text = "";
            targetTimeText.text = "";
            playerTimeText.text = "";
        }

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
            // rating.text = RATING[0];
            ratingImage.sprite = ratings[0];
        }
        else
        {
            // rating.text = RATING[stars - 1];
            ratingImage.sprite = ratings[stars - 1];
        }
    }

    private void ActivateStarIcon(int _stars)
    {
        foreach (GameObject icon in starIcon)
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
