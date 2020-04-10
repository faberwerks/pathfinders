using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Collectible
{
    //private AudioSource getTreasure;
    //public GameObject getTreasureObject;

    //private void Start()
    //{
    //    getTreasureObject = Instantiate(getTreasureObject);
    //    getTreasure = getTreasureObject.GetComponent<AudioSource>();
    //}
    //It add point to the treasure collected

    int point = 0;

    //CheckPointSaveData data;

    private void Start()
    {
        point = 1;
        // commented to try new system
        //data = Blackboard.Instance.LevelManager.GetComponent<CheckPointSaveData>();
        //AddToSaveData();
    }

    //commented to try new system
    //Samuel 7 April 2020 - Add
    //private void AddToSaveData()
    //{
    //    Debug.Log("Added");

    //    data.AddItem(transform.position.x , transform.position.y , false , "treasure");
    //}

    protected override void OnCollected()
    {
        //getTreasure.Play();
        Blackboard.Instance.LevelManager.TreasureCollected += point;
        point = 0;
        Debug.Log(Blackboard.Instance.LevelManager.TreasureCollected);
        #region commented
        //Commented to try new system
        //Samuel 9 april 2020 - add for save checkpoint
        //data.EditState(transform.position.x, transform.position.y, true, "treasure");
        #endregion
    }
}
