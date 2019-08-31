using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    [SerializeField]
    private Transform target;

    //list of characters in one teleporter
    [SerializeField]
    private List<Transform> characters;

    // Start is called before the first frame update
    void Start()
    {
        characters = new List<Transform>();
    }

    // Update is called once per frame
    private void Update()
    {
        //only called when button is clicked
        if(Blackboard.instance.LevelManager.IsInteracting && characters.Count != 0)
        {
            Teleport();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            characters.Add(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            characters.Remove(collision.transform);
        }
    }

    //teleport player when interact button clicked
    private void Teleport()
    {
        foreach (Transform character in characters)
        {
            character.position = target.position;
            //Debug.Log("tele");
        }
        //characters.Dequeue().transform.position = target.position;
        Blackboard.instance.LevelManager.IsInteracting = false;
    }
}
