using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRelicHandler : MonoBehaviour
{
    public Toggler tutorialLever;

    private void Start()
    {
        Time.timeScale = 1;
        tutorialLever.Interact();
        Time.timeScale = 0;
    }
}
