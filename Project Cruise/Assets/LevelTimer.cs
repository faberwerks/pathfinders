using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    public bool gameState;
    public float timer;
    public GameObject endPanel;
    public Text timerText;
    public Text finalTime;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        gameState = true;
    }

    // Update is called once per frame
    void Update()
    {
        IncrementTimer();
        if(!gameState)
        {
            EndGame();
        }
    }

    public void IncrementTimer()
    {
        timer += 1 * Time.deltaTime;
        timerText.text = "Timer: " + Mathf.Round(timer).ToString();
    }

    public void SetState()
    {
        gameState = false;
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        endPanel.SetActive(true);
        finalTime.text = "Final Time: " + Mathf.Round(timer).ToString();
    }
}
