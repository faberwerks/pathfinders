using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A component to handle triggered timers.
/// </summary>
public class TriggeredTimer : MonoBehaviour
{
    public List<GameObject> pathObjects;

    public float countdownTime = 5.0f;
    public float CountdownTimer { get; set; }
    public bool HasRun { get; set; }

    private Image fillImage;

    private void Start()
    {
        HasRun = false;
        pathObjects = new List<GameObject>();

        CountdownTimer = countdownTime;
        fillImage = Blackboard.Instance.LevelManager.triggeredTimerFillImage;

        CheckPointSaveData data = Blackboard.Instance.LevelManager.checkPointSaveData;
        data.AddTriggeredTimer(this, countdownTime);
    }

    /// <summary>
    /// Destroys path objects and starts timer.
    /// </summary>
    public void StartTimer()
    {
        if (pathObjects.Count > 0)
        {
            foreach (GameObject pathObject in pathObjects)
            {
                pathObjects.Remove(pathObject);
                Destroy(pathObject);
            }
        }

        StartCoroutine(CTimer(countdownTime));
    }

    private IEnumerator CTimer(float duration)
    {
        Blackboard.Instance.LevelManager.triggeredTimerTime.gameObject.SetActive(true);
        Blackboard.Instance.LevelManager.triggeredTimerTime.text = CountdownTimer.ToString("0");
        float value = 0;

        while (CountdownTimer > 0.0f)
        {
            CountdownTimer -= Time.deltaTime;
            Blackboard.Instance.LevelManager.triggeredTimerTime.text = CountdownTimer.ToString("0");
            value = CountdownTimer / countdownTime;
            fillImage.fillAmount = value;
            yield return null;
        }

        Blackboard.Instance.LevelManager.EndLevelTimer();
        HasRun = true;
        Blackboard.Instance.LevelManager.Lose();
    }

    public void RestartTriggeredTimer(float duration)
    {
        HasRun = false;
        StartCoroutine(CTimer(duration));
    }
}
