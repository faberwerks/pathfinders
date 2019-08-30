using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Queue<Transform> Characters;

    // Start is called before the first frame update
    void Start()
    {
        Characters = new Queue<Transform>();
    }

    // Update is called once per frame
    private void Update()
    {
        //teleport player when interact button clicked
        if(Blackboard.instance.LevelManager.IsInteracting)
        {
            //foreach(Transform character in Characters)
            //{
            //    character.position = target.position;
            //    Debug.Log("tele");
            //}
            Characters.Dequeue().transform.position = target.position;
            Blackboard.instance.LevelManager.IsInteracting = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Characters.Enqueue(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Characters.Dequeue();
        }
    }
    

}
