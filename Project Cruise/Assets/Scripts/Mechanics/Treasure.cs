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

    private void Start()
    {
        point = 1;
        AddToSaveData();
    }

    //Samuel 7 April 2020 - Add
    private void AddToSaveData()
    {
        Debug.Log("Added");
        CheckPointSaveData data = Blackboard.Instance.LevelManager.GetComponent<CheckPointSaveData>();
        data.AddItem(transform.position.x , transform.position.y , false , "treasure");
    }

    protected override void OnCollected()
    {
        //getTreasure.Play();
        Blackboard.Instance.LevelManager.TreasureCollected += point;
        point = 0;
        Debug.Log(Blackboard.Instance.LevelManager.TreasureCollected);
    }
}
