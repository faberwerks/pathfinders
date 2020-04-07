using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSaveData : MonoBehaviour
{

    struct Character  
    {
        int x;
        int y;

        public Character(int x , int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    struct Lever 
    {
        int x;
        int y;
        bool istriggered;

        public Lever(int x, int y,bool istriggered)
        {
            this.x = x;
            this.y = y;
            this.istriggered = istriggered;
        }
    }

    struct Obstacle
    {
        int x;
        int y;
        bool istriggered;

        public Obstacle(int x , int y, bool istriggered)
        {
            this.x = x;
            this.y = y;
            this.istriggered = istriggered;
        }
    }

    struct TriggeredTimer
    {
        int x;
        int y;
        bool isTaken;

        public TriggeredTimer(int x , int y , bool isTaken)
        {
            this.x = x;
            this.y = y;
            this.isTaken = isTaken;
        }
    }

    struct Treasure
    {
        int x;
        int y;
        bool istriggered;

        public Treasure(int x, int y , bool istriggered)
        {
            this.x = x;
            this.y = y;
            this.istriggered = istriggered;
        }
    }

    struct Relic
    {
        bool isTaken;

        public void SetIstaken(bool isTaken)
        {
            this.isTaken = isTaken;
        }
    }
    
    struct Door
    {
        int x;
        int y;
        bool istriggered;

        public Door(int x, int y , bool istriggered)
        {
            this.x = x;
            this.y = y;
            this.istriggered = istriggered;
        }
    }

    struct Key
    {
        int id;
        bool isTaken;

        public Key(int id, bool isTaken)
        {
            this.id = id;
            this.isTaken = isTaken;
        }
    }

    #region List
    List<Character> Characters;
    List<Lever> Levers;
    List<Obstacle> Obstacles;
    List<TriggeredTimer> Triggeredtimers;
    List<Treasure> Treasures;
    List<Door> Doors;
    List<Key> Keys;
    #endregion

    Relic relic;

    // Start is called before the first frame update
    void Start()
    {
        #region init list
        Characters = new List<Character>();
        Levers = new List<Lever>();
        Obstacles = new List<Obstacle>();
        Treasures = new List<Treasure>();
        Doors = new List<Door>();
        Keys = new List<Key>();
        #endregion

        relic = new Relic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // to Add character to list
    public void AddCharacterData(int x, int y)
    {
        Characters.Add(new Character(x, y));
    }

    public void AddItem(int x, int y , bool triggered , string type)
    {
        if(type.Equals("lever",StringComparison.InvariantCultureIgnoreCase))
        {
            Levers.Add(new Lever(x, y, triggered));
        }
        else if(type.Equals("obstacle", StringComparison.InvariantCultureIgnoreCase))
        {
            Obstacles.Add(new Obstacle(x, y, triggered));
        }
        else if (type.Equals("triggeredtimer", StringComparison.InvariantCultureIgnoreCase))
        {
            Triggeredtimers.Add(new TriggeredTimer(x, y, triggered));
        }
        else if (type.Equals("treasure", StringComparison.InvariantCultureIgnoreCase))
        {
            Treasures.Add(new Treasure(x, y, triggered));
        }
        else if (type.Equals("door", StringComparison.InvariantCultureIgnoreCase))
        {
            Doors.Add(new Door(x, y, triggered));
        }
    }

    public void SetRelic(bool isTaken)
    {
        relic.SetIstaken(isTaken);
    }

    public void AddItem()
    {

    }

}
