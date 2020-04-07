using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSaveData : MonoBehaviour
{

    struct Character  
    {
        float x;
        float y;
        float lastX;
        float lastY;

        public Character(float x , float y)
        {
            this.x = x;
            this.y = y;
            this.lastX = x;
            this.lastY = y;
        }

        public float GetX()
        {
            return x;
        }

        public float GetY()
        {
            return y;
        }

        public float GetLastX()
        {
            return lastX;
        }

        public float GetLastY()
        {
            return lastY;
        }

        public void SetPosition(float x , float y)
        {
            this.x = x;
            this.y = y;
            lastX = x;
            lastY = y;
        }

    }

    struct Lever 
    {
        float x;
        float y;
        bool isTriggered;

        public Lever(float x, float y,bool istriggered)
        {
            this.x = x;
            this.y = y;
            this.isTriggered = istriggered;
        }

        public float GetX()
        {
            return x;
        }

        public float GetY()
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
        float x;
        float y;
        bool isTriggered;

        public Obstacle(float x , float y, bool isTriggered)
        {
            this.x = x;
            this.y = y;
            this.isTriggered = isTriggered;
        }

        public float GetX()
        {
            return x;
        }

        public float GetY()
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
        float x;
        float y;
        bool isTriggered;

        public TriggeredTimer(float x , float y , bool isTriggered)
        {
            this.x = x;
            this.y = y;
            this.isTriggered = isTriggered;
        }

        public float GetX()
        {
            return x;
        }

        public float GetY()
        {
            return y;
        }

        public void SetIsTriggered(bool isTriggered)
        {
            this.isTriggered = isTriggered;
        }

    }

    public struct Treasure
    {
        float x;
        float y;
        bool isTriggered;

        public Treasure(float x, float y , bool isTriggered)
        {
            this.x = x;
            this.y = y;
            this.isTriggered = isTriggered;
        }

        public float GetX()
        {
            return x;
        }

        public float GetY()
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
        float x;
        float y;
        bool isTriggered;

        public Door(float x, float y , bool isTriggered)
        {
            this.x = x;
            this.y = y;
            this.isTriggered = isTriggered;
        }

        public float GetX()
        {
            return x;
        }

        public float GetY()
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
    public List<Treasure> Treasures;
    List<Door> Doors;
    List<Key> Keys;
    #endregion

    Relic relic;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("init all");
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
    public void AddCharacterData(float x, float y)
    {
        Characters.Add(new Character(x, y));
    }

    public void AddItem(float x, float y , bool triggered , string type)
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
            try
            {
                Treasures.Add(new Treasure(x, y, triggered));
            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
            }
            
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

    public void EditState(float x, float y ,bool state , string type)
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

    public void EditPlayerPostion(float x , float y , float LastX , float LastY)
    {
        foreach(Character character in Characters)
        {
            if(character.GetLastX() == LastX && character.GetLastY() == LastY)
            {
                character.SetPosition(x, y);
            }
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
