/// <summary>
/// Static class holding current playthrough data.
/// </summary>
public class GameData
{
    private static GameData instance;
    public static GameData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameData();
            }
            return instance;
        }
    }

    public GameData()
    {
        previousSceneID = 0;
        interstitialAdsCounter = 3;
        currTreasuresCollected = 0;
        currIsRelicCollected = false;
        currLevelTime = 0.0f;
        currLevelID = 0;
        coinsEarned = 0;
        saveData = new SaveData();
        tutorialDebug = false;
    }

    public int previousSceneID = 0;
    public int interstitialAdsCounter = 3;
    public int currTreasuresCollected = 0;
    public bool currIsRelicCollected = false;
    public float currLevelTime = 0.0f;
    public int currLevelID = 0;
    public int coinsEarned = 0;
    public int starsEarned = 0;
    public SaveData saveData = null;
    // for tutorial debugging
    public bool tutorialDebug = false;
}
