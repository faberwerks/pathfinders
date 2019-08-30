using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    [SerializeField]
    private Transform target;

    //list of characters in one teleporter
    [SerializeField]
    private List<Transform> Characters;

    // Start is called before the first frame update
    void Start()
    {
        Characters = new List<Transform>();
    }

    // Update is called once per frame
    private void Update()
    {
        //only called when button is clicked
        if(Blackboard.instance.LevelManager.IsInteracting && Characters.Count != 0)
        {
            Teleport();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Characters.Add(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Characters.Remove(collision.transform);
        }
    }

    //teleport player when interact button clicked
    private void Teleport()
    {
        foreach (Transform character in Characters)
        {
            character.position = target.position;
            //Debug.Log("tele");
        }
        //Characters.Dequeue().transform.position = target.position;
        Blackboard.instance.LevelManager.IsInteracting = false;
    }
}
