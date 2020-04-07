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

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }

        public void SetPosition(int x , int y)
        {
            this.x = x;
            this.y = y;
        }

    }

    struct Lever 
    {
        int x;
        int y;
        bool isTriggered;

        public Lever(int x, int y,bool istriggered)
        {
            this.x = x;
            this.y = y;
            this.isTriggered = istriggered;
        }

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }

        public void SetIsTriggered(bool isTriggered)
        {
            this.isTriggered = isTriggered;
        }

    }

    struct Obstacle
    {
        int x;
        int y;
        bool isTriggered;

        public Obstacle(int x , int y, bool isTriggered)
        {
            this.x = x;
            this.y = y;
            this.isTriggered = isTriggered;
        }

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }

        public void SetIsTriggered(bool isTriggered)
        {
            this.isTriggered = isTriggered;
        }

    }

    struct TriggeredTimer
    {
        int x;
        int y;
        bool isTriggered;

        public TriggeredTimer(int x , int y , bool isTriggered)
        {
            this.x = x;
            this.y = y;
            this.isTriggered = isTriggered;
        }

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }

        public void SetIsTriggered(bool isTriggered)
        {
            this.isTriggered = isTriggered;
        }

    }

    struct Treasure
    {
        int x;
        int y;
        bool isTriggered;

        public Treasure(int x, int y , bool isTriggered)
        {
            this.x = x;
            this.y = y;
            this.isTriggered = isTriggered;
        }

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }

        public void SetIsTriggered(bool isTriggered)
        {
            this.isTriggered = isTriggered;
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
        bool isTriggered;

        public Door(int x, int y , bool isTriggered)
        {
            this.x = x;
            this.y = y;
            this.isTriggered = isTriggered;
        }

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }

        public void SetIsTriggered(bool isTriggered)
        {
            this.isTriggered = isTriggered;
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

        public void SetisTaken(bool isTaken)
        {
            this.isTaken = isTaken;
        }

        public int GetID()
        {
            return id;
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

    public void AddKey(int id, bool isTaken)
    {
        Keys.Add(new Key(id, isTaken));
    }

    public void EditState(int x, int y ,bool state , string type)
    {
        if (type.Equals("lever", StringComparison.InvariantCultureIgnoreCase))
        {
            foreach(Lever data in Levers)
            {
                if(data.GetX() == x && data.GetY() == y)
                {
                    data.SetIsTriggered(state);
                    break;
                }
            }
        }
        else if (type.Equals("obstacle", StringComparison.InvariantCultureIgnoreCase))
        {
            foreach (Obstacle data in Obstacles)
            {
                if (data.GetX() == x && data.GetY() == y)
                {
                    data.SetIsTriggered(state);
                    break;
                }
            }
        }
        else if (type.Equals("triggeredtimer", StringComparison.InvariantCultureIgnoreCase))
        {
            foreach (TriggeredTimer data in Triggeredtimers)
            {
                if (data.GetX() == x && data.GetY() == y)
                {
                    data.SetIsTriggered(state);
                    break;
                }
            }
        }
        else if (type.Equals("treasure", StringComparison.InvariantCultureIgnoreCase))
        {
            foreach (Treasure data in Treasures)
            {
                if (data.GetX() == x && data.GetY() == y)
                {
                    data.SetIsTriggered(state);
                    break;
                }
            }
        }
        else if (type.Equals("door", StringComparison.InvariantCultureIgnoreCase))
        {
            foreach (Door data in Doors)
            {
                if (data.GetX() == x && data.GetY() == y)
                {
                    data.SetIsTriggered(state);
                    break;
                }
            }
        }
    }

    public void EditPlayerPostion(int x , int y)
    {
        foreach(Character character in Characters)
        {
            //edit later
            character.SetPosition(x, y);
        }
    }

    public void SetKeyIsTaken(int id , bool isTaken)
    {
        foreach(Key key in Keys)
        {
            if(key.GetID() == id)
            {
                key.SetisTaken(isTaken);
                break;
            }
        }
    }

}
