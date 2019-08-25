using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Parent of all collectible item
/// </summary>
public class Collectible : MonoBehaviour
{
    //Called when Player touch the collectible
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnCollected();
            Destroy(gameObject);
        }
    }

    //What the collected give when it touch by the Player
    protected virtual void OnCollected()
    {

    }
}
