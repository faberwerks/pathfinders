using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{
    // Update is called once per frame

    private Queue<GameObject> tutorialLayers;
    private GameObject currLayer;

    private void Start()
    {
        tutorialLayers = new Queue<GameObject>();
        foreach(Transform transform in GetComponentsInChildren<Transform>())
        {
            if (transform.gameObject.CompareTag("TutorialLayer"))
            {
                tutorialLayers.Enqueue(transform.gameObject);
                transform.gameObject.SetActive(false);
            }
        }
        currLayer = tutorialLayers.Dequeue();
        currLayer.SetActive(true);
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (tutorialLayers.Count == 0)
            {
                Time.timeScale = 1f;
                Destroy(gameObject);
            }
            else
            {
                currLayer.SetActive(false);
                GameObject nextLayer = tutorialLayers.Dequeue();
                currLayer = nextLayer;
                currLayer.SetActive(true);
            }
        }
        
    }
}
