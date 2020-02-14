using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{
    // Update is called once per frame

    private void Start()
    {
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            Destroy(gameObject);
            Time.timeScale = 1f;
        }
        
    }
}
