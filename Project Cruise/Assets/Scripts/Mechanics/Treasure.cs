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
    protected override void OnCollected()
    {
        //getTreasure.Play();
        Blackboard.Instance.LevelManager.TreasureCollected += 1;
        Debug.Log(Blackboard.Instance.LevelManager.TreasureCollected);
    }
}
