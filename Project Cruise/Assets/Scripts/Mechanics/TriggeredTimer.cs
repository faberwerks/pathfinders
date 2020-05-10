using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A component to handle triggered timers.
/// </summary>
public class TriggeredTimer : MonoBehaviour
{
    public List<GameObject> pathObjects;

    public float countdownTime = 5.0f;
    public float CountdownTimer { get; set; }
    private bool timerIsActive;

    private void Start()
    {
        pathObjects = new List<GameObject>();

        CountdownTimer = countdownTime;

        timerIsActive = false;

        CheckPointSaveData data = Blackboard.Instance.LevelManager.checkPointSaveData;
        data.AddTriggeredTimer(this);

        Blackboard.Instance.LevelManager.triggeredTimerTime.text = "";
    }

    // Update is called once per frame
    private void Update()
    {
        if (timerIsActive && CountdownTimer > 0)
        {
            CountdownTimer -= Time.deltaTime;

            Blackboard.Instance.LevelManager.triggeredTimerTime.text = CountdownTimer.ToString("0");

            if (CountdownTimer <= 0)
            {
                Debug.Log("LOSE.");
                Blackboard.Instance.LevelManager.Lose();
            }
        }
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

        timerIsActive = true;
    }
}
