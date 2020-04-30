using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSaveData
{
    struct Character
    {
        public GameObject CharacterObject { get; set; }
        public float X { get; set; }
        public float Y { get; set; }

        // CONSTRUCTOR
        public Character(GameObject character , float x, float y)
        {
            this.CharacterObject = character;
            this.X = x;
            this.Y = y;
        }
        
        public void SavePosition(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

    }

    struct TriggeredTimerData
    {
        public TriggeredTimer TriggeredTimerObject { get; set; }
        public float Time { get; set; }

        public TriggeredTimerData(TriggeredTimer triggeredTimer, float time)
        {
            TriggeredTimerObject = triggeredTimer;
            Time = time;
        }
    }

    struct Timer
    {
        public LevelTimer TimerObject { get; set; }
        public float Time { get; set; }

        public Timer(LevelTimer levelTimer)
        {
            TimerObject = levelTimer;
            Time = levelTimer.timer;
        }
    }

    private List<Character> characters;
    private List<TriggeredTimerData> triggeredTimers;
    private Timer timer;

    public CheckPointSaveData(LevelTimer levelTimer)
    {
        characters = new List<Character>();
        triggeredTimers = new List<TriggeredTimerData>();
        timer = new Timer();
        timer.TimerObject = levelTimer;
    }

    public void AddCharacter(GameObject player, float x, float y)
    {
        Debug.Log("[DEBUG] AddCharacter called.");
        characters.Add(new Character(player, x, y));
    }

    public void AddTriggeredTimer(TriggeredTimer triggeredTimer)
    {
        Debug.Log("[DEBUG] AddTriggeredTimer called.");
        triggeredTimers.Add(new TriggeredTimerData(triggeredTimer, 0.0f));
    }

    private void SavePlayerPostions()
    {
        Debug.Log("[DEBUG] SavePlayerPositions called.");

        Character temp;

        for (int i = 0; i < characters.Count;i++)
        {
            temp = new Character(characters[i].CharacterObject, characters[i].CharacterObject.transform.position.x, characters[i].CharacterObject.transform.position.y);
            characters[i] = temp;
        }
    }

    private void SaveTriggeredTimerTimes()
    {
        Debug.Log("[DEBUG] SaveTriggeredTimerTimes called.");

        TriggeredTimerData temp;

        for (int i = 0; i < triggeredTimers.Count; i++)
        {
            temp = new TriggeredTimerData(triggeredTimers[i].TriggeredTimerObject, triggeredTimers[i].TriggeredTimerObject.CountdownTimer);
            triggeredTimers[i] = temp;
        }
    }

    private void SaveTimerTime()
    {
        Timer temp = new Timer(timer.TimerObject);
        timer = temp;
    }

    public void SaveCheckPointSaveData()
    {
        SavePlayerPostions();
        SaveTriggeredTimerTimes();
        SaveTimerTime();
    }

    public void LoadCheckPointSaveData()
    {
        Character tempChar;
        Vector3 tempPos = Vector3.zero;
        // reposition Characters
        for (int i = 0; i < characters.Count; i++)
        {
            tempChar = characters[i];
            tempPos.x = tempChar.X;
            tempPos.y = tempChar.Y;
            tempPos.z = tempChar.CharacterObject.transform.localPosition.z;

            tempChar.CharacterObject.transform.localPosition = tempPos;
            tempChar.CharacterObject.GetComponent<PlayerController>().ResetAnimation();
        }

        // reset timer to last saved time
        timer.TimerObject.timer = timer.Time;

        // reset triggered timers to last saved time
        for (int i = 0; i < triggeredTimers.Count; i++)
        {
            triggeredTimers[i].TriggeredTimerObject.CountdownTimer = triggeredTimers[i].Time;
        }
    }

    //public void SetTimer(float time)
    //{
    //    Debug.Log("SetTimer Called");
    //    triggeredTimer.Time = time;
    //}

    //public float GetTimer(float time)
    //{
    //    return triggeredTimer.Time;
    //}

    //public void Save(float time)
    //{
    //    Debug.Log("save called");
    //    SavePlayerPostion();
    //    SetTimer(time);
    //}

    //public void Load()
    //{
    //    Character temp = null;
    //    Vector3 tempPos = Vector3.zero;
    //    // reposition Characters
    //    for(int i = 0; i < characters.Count;i++)
    //    {
    //        temp = characters[i];
    //        tempPos.x = temp.X;
    //        tempPos.y = temp.Y;
    //        tempPos.z = temp.CharacterObject.transform.localPosition.z;

    //        temp.CharacterObject.transform.localPosition = tempPos;
    //        // TODO call method on Character to return to normal animation
    //    }

    //    // TODO reset timer to last saved time
    //    // TODO reset triggered timers to last saved time
    //}

}
