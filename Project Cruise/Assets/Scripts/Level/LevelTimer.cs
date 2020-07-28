using UnityEngine;

/// <summary>
/// Component that counts how long the player has played the level.
/// </summary>
public class LevelTimer : MonoBehaviour
{
    public float timer;
    private bool timeHasEnded = true;

    private void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!timeHasEnded)
        {
            timer += Time.deltaTime;
        }
    }

    /// <summary>
    /// Publically accessible method to end the timer.
    /// </summary>
    public void EndTimer()
    {
        timeHasEnded = true;
        GameData.Instance.currLevelTime = timer;
    }

    /// <summary>
    /// Publically accessible method to end the timer with additional argument to check <0.2s tolerance
    /// </summary>
    public void EndTimer(float targetTime)
    {
        timeHasEnded = true;
        if (timer > targetTime && timer - targetTime < 0.2f) timer = targetTime;
        GameData.Instance.currLevelTime = timer;
    }

    /// <summary>
    /// Publically accessible method to enable the timer.
    /// </summary>
    /// 27 May 2020 Samuel - Add
    public void EnableTimer()
    {
        timeHasEnded = false;
    }
}
