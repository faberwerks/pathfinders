using UnityEngine;

/// <summary>
/// Component that counts how long the player has played the level.
/// </summary>
public class LevelTimer : MonoBehaviour
{
    public float timer;
    private bool timeHasEnded;

    private void Start()
    {
        timer = 0f;
        timeHasEnded = true;
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
    /// Publically accessible method to enable the timer.
    /// </summary>
    /// 27 May 2020 Samuel - Add
    public void EnableTimer()
    {
        timeHasEnded = false;
    }
}
