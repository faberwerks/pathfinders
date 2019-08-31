using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private GameObject player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            Debug.Log(player);
        }
    }

    private void OnDisable()
    {
        if(player != null)
        {
            player.GetComponent<PlayerController>().hasKey = false;
            Debug.Log(player.GetComponent<PlayerController>().hasKey);
        }
    }
}
