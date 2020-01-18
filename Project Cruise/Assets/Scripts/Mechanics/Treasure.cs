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
    }

    protected override void OnCollected()
    {
        //getTreasure.Play();
        Blackboard.Instance.LevelManager.TreasureCollected += point;
        Debug.Log(Blackboard.Instance.LevelManager.TreasureCollected);
        point = 0;
    }
}
