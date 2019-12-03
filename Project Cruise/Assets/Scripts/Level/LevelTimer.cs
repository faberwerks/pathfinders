using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    //public bool gameHasEnded;
    private float timer;
    private bool timeHasEnded;
    //public GameObject endPanel;
    //public TMP_Text timerText;
    //public TMP_Text finalTime;
    // Start is called before the first frame update
    private void Start()
    {
        timer = 0f;
        timeHasEnded = false;
        //gameHasEnded = true; //Determines whether the game ends. True = start, False = end
    }

    // Update is called once per frame
    private void Update()
    {
        if (!timeHasEnded)
        {
            IncrementTimer();
        }
        //if(!gameHasEnded)
        //{
        //    EndGame();
        //}
    }

    private void IncrementTimer()
    {
        timer += Time.deltaTime;
        //timerText.text = timer.ToString("F2");
    }

    //public void SetGameEnd() //For button use. sets gameState to false to end the game
    //{
    //    //gameHasEnded = false;
    //}

    //private void EndGame()
    //{
    //    Time.timeScale = 0;
    //    endPanel.SetActive(true);
    //    finalTime.text = "Final Time: " + Mathf.Round(timer).ToString();
    //}

    public void EndTimer()
    {
        timeHasEnded = true;
        GameData.Instance.currLevelTime = timer;
    }
}
