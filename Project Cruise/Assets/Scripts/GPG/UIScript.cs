using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{
    public static UIScript Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    [SerializeField]
    private TMP_Text pointsTxt;
    [SerializeField]
    private TMP_Text highscoreTxt;

    public void GetPoint()
    {
        ManagerScript.Instance.IncrementCounter();
    }

    public void Restart()
    {
        ManagerScript.Instance.RestartGame();
    }

    public void Increment()
    {
        PlayGamesScript.IncrementAchievement(GPGSIds.achievement_incremental_achievement, 5);
    }

    public void Unlock()
    {
        PlayGamesScript.UnlockAchievement(GPGSIds.achievement_working_achievement);
    }

    public void ShowAchievements()
    {
        PlayGamesScript.ShowAchievementsUI();
    }

    //public void ShowLeaderboard()
    //{
    //    PlayGamesScript.ShowLeaderboardsUI();
    //}

    public void UpdatePointsText()
    {
        pointsTxt.text = ManagerScript.Counter.ToString();
    }

    public void UpdateHighscoreText()
    {
        highscoreTxt.text = CloudVariables.Highscore.ToString();
    }
}
