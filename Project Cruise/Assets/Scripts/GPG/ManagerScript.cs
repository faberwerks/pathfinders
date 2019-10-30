using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    public static ManagerScript Instance { get; private set; }
    public static int Counter { get; private set; }

    // Start is called before the first frame update
    private void Start()
    {
        Instance = this;
        UIScript.Instance.UpdateHighscoreText();
    }

    public void IncrementCounter()
    {
        Counter++;
        UIScript.Instance.UpdatePointsText();
    }

    public void RestartGame()
    {
        //PlayGamesScript.AddScoreToLeaderboard(GPGSIds.leaderboard_test_leaderboard, Counter);

        if (Counter > CloudVariables.Highscore)
        {
            CloudVariables.Highscore = Counter;
            PlayGamesScript.Instance.SaveData();
            UIScript.Instance.UpdateHighscoreText();
        }

        Counter = 0;
        UIScript.Instance.UpdatePointsText();
    }
}
