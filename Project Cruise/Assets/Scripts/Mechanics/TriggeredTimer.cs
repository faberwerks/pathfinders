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
    private float countdownTimer;
    private bool timerIsActive;

    private void Start()
    {
        pathObjects = new List<GameObject>();

        countdownTimer = countdownTime;

        timerIsActive = false;
        //to clear the ui text
        Blackboard.Instance.LevelManager.TriggeredTimerUi.text = "";
    }

    // Update is called once per frame
    private void Update()
    {
        if (timerIsActive && countdownTimer > 0)
        {
            countdownTimer -= Time.deltaTime;
            //Samuel - Add to show the timer in the ui
            Blackboard.Instance.LevelManager.TriggeredTimerUi.text = countdownTimer.ToString();

            if (countdownTimer <= 0)
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
