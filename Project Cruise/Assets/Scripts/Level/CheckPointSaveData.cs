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

    struct TriggeredTimer
    {
        public GameObject TriggeredTimerObject { get; set; }
        public float Time { get; set; }

        public TriggeredTimer(GameObject triggeredTimer)
        {
            TriggeredTimerObject = triggeredTimer;
            Time = 0.0f;
        }
    }

    struct Timer
    {
        public LevelTimer TimerObject { get; set; }
        public float Time { get; set; }
    }

    List<Character> characters;
    List<TriggeredTimer> triggeredTimers;
    Timer timer;

    public CheckPointSaveData(LevelTimer levelTimer)
    {
        characters = new List<Character>();
        triggeredTimers = new List<TriggeredTimer>();
        timer = new Timer();
        timer.TimerObject = levelTimer;
    }

    public void AddCharacter(GameObject player, float x, float y)
    {
        Debug.Log("[DEBUG] AddCharacter called.");
        characters.Add(new Character(player, x, y));
    }

    public void AddTriggeredTimer(GameObject triggeredTimer)
    {
        Debug.Log("[DEBUG] AddTriggeredTimer called.");
        triggeredTimers.Add(new TriggeredTimer(triggeredTimer));
    }

    public void SavePlayerPostion()
    {
        Debug.Log("Save player called");

        for(int i = 0; i < characters.Count;i++)
        {
            Character temp = new Character(characters[i].CharacterObject, characters[i].CharacterObject.transform.position.x, characters[i].CharacterObject.transform.position.y);
            characters[i] = temp;
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
