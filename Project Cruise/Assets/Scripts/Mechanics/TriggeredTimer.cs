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
    }

    // Update is called once per frame
    private void Update()
    {
        if (timerIsActive && CountdownTimer > 0)
        {
            CountdownTimer -= Time.deltaTime;

            if (CountdownTimer <= 0)
            {
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
